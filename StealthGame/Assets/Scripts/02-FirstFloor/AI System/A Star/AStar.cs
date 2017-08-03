using UnityEngine;
using System.Collections;

public class AStar {
    public static PriorityQueue closedList, openList;

    /// <summary>
    /// Next we implement a method called HeuristicEstimateCost to calculate the cost
    /// between the two nodes. The calculation is simple. We just find the direction vector
    /// between the two by subtracting one position vector from another. The magnitude of
    /// this resultant vector gives the direct distance from the current node to the goal node
    /// </summary>
    /// <param name="curNode"></param>
    /// <param name="goalNode"></param>
    /// <returns></returns>
    private static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector3 vecCost = curNode.position - goalNode.position;
        return vecCost.magnitude;
    }

    /// <summary>
    /// Main FindPath method
    /// 1. Get the first node of our openList. Remember our openList of nodes is
    ///    always sorted every time a new node is added. So the first node is always the
    ///    node with the least estimated cost to the goal node.
    /// 2. Check if the current node is already at the goal node. If so, exit the while
    ///    loop and build the path array.
    /// 3. Create an array list to store the neighboring nodes of the current node being
    ///    processed. Use the GetNeighbours method to retrieve the neighbors from
    ///    the grid.
    /// 4. For every node in the neighbors array, we check if it's already in the
    ///    closedList. If not, put it in the calculate the cost values, update the node
    ///    properties with the new cost values as well as the parent node data and
    ///    put it in openList.
    /// 5. Push the current node to closedList and remove it from openList. Go back
    ///    to step 1.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <returns></returns>
    public static ArrayList FindPath(Node start, Node goal)
    {
        openList = new PriorityQueue();
        openList.Push(start);
        start.nodeTotalCost = 0.0f;
        start.estimatedCost = HeuristicEstimateCost(start,goal);

        closedList = new PriorityQueue();
        Node node = null;

        //We initialize our open and closed lists. Starting with the start node, we put it in our
        //open list. Then we start processing our open list.
        while (openList.Length != 0)
        {
            node = openList.First();

            //Check if the current node is the goal node
            if (node.position == goal.position)
                return CalculatePath(node);

            //Create an ArrayList to store the neighboring nodes
            ArrayList neighbours = new ArrayList();

            GridManager.instance.GetNeighbours(node, neighbours);

            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];

                if (!closedList.Contains(neighbourNode))
                {
                    float cost = HeuristicEstimateCost(node, neighbourNode);

                    float totalCost = node.nodeTotalCost + cost;
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);

                    neighbourNode.nodeTotalCost = totalCost;
                    neighbourNode.parent = node;
                    neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;

                    if (!openList.Contains(neighbourNode))
                        openList.Push(neighbourNode);
                }
            }

            //Push the current node to the closed list
            closedList.Push(node);
            //and remove it from openList
            openList.Remove(node);
        }

        if (node.position != goal.position)
        {
            Debug.LogError("Goal not Found!");
            return null;
        }

        return CalculatePath(node);
    }

    /// <summary>
    /// The CalculatePath method traces through each node's parent node object and builds
    /// an array list. It gives an array list with nodes from target node to start node. Since we
    /// want a path array from start node to target node we just call the Reverse method.
    /// </summary>
    /// <param name="node"></param>
    /// <returns></returns>
    private static ArrayList CalculatePath(Node node)
    {
        ArrayList list = new ArrayList();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }

        list.Reverse();
        list = SmoothPath(list);

        return list;
    }

    private static ArrayList SmoothPath(ArrayList aList)
    {
        //List containing path with rounded corners...
        ArrayList smoothList = new ArrayList();
        float radius = 2.0f;
        int lCount = aList.Count-2;
        int i = 0;
        while(i < lCount)
        {
            int j = i + 1;
            int k = i + 2;

            //Corner points
            Node P1 = (Node)aList[i];
            Node P2 = (Node)aList[j];
            Node P3 = (Node)aList[k];

            //Calculate angle between vectors
            float angle = Mathf.Atan2(P2.position.z - P1.position.z, P2.position.x - P1.position.x) -
                           Mathf.Atan2(P2.position.z - P3.position.z, P2.position.x - P3.position.x);


            //Vectors form straight line
            if (angle == Mathf.PI || angle == -Mathf.PI)
            {
                //just add point to list
                smoothList.Add(P1);

                //Point is added to the path, go to next
                i = i + 1;
            }
            //Vectors form corner
            else
            { 
                // The length of segment between angular point and the
                // points of intersection with the circle of a given radius
                float tan = Mathf.Abs(Mathf.Tan(angle / 2));
                float segment = radius / tan;

                //Check the segment
                float lLength1 = GetLength(P2.position.x - P1.position.x, P2.position.z - P1.position.z);
                float lLength2 = GetLength(P2.position.x - P3.position.x, P2.position.z - P3.position.z);

                float lLength = Mathf.Min(lLength1, lLength2);

                if (segment > lLength)
                {
                    segment = lLength;
                    radius = (lLength * tan);
                }

                // Points of intersection are calculated by the proportion between 
                // the coordinates of the vector, length of vector and the length of the segment.
                Node p1Cross = GetProportionPoint(P2, segment, lLength1, P2.position.x - P1.position.x, P2.position.z - P1.position.z);
                Node p2Cross = GetProportionPoint(P2, segment, lLength2, P2.position.x - P3.position.x, P2.position.z - P3.position.z);

                // Calculation of the coordinates of the circle  center by the addition of angular vectors.
                float dx = P2.position.x * 2 - p1Cross.position.x - p2Cross.position.x;
                float dz = P2.position.z * 2 - p1Cross.position.z - p2Cross.position.z;

                float L = GetLength(dx, dz);
                float d = GetLength(segment, radius);

                Node circleCenter = GetProportionPoint(P2, d, L, dx, dz);

                //StartAngle and EndAngle of arc
                float startAngle = Mathf.Atan2(p1Cross.position.z - circleCenter.position.z, p1Cross.position.x - circleCenter.position.x);
                float endAngle = Mathf.Atan2(p2Cross.position.z - circleCenter.position.z, p2Cross.position.x - circleCenter.position.x);

                //Sweep angle
                float sweepAngle = endAngle - startAngle;

                //Some additional checks
                if (sweepAngle < 0)
                {
                    startAngle = endAngle;
                    sweepAngle = -sweepAngle;
                }

                if (sweepAngle > Mathf.PI)
                    sweepAngle = Mathf.PI - sweepAngle;

                //One point for each degree. But in some cases it will be necessary 
                // to use more points. Just change a degreeFactor.
                float degreeFactor = 180 / Mathf.PI;
                int pointCount = (int)Mathf.Abs(sweepAngle * degreeFactor);
                float sign = Mathf.Sign(sweepAngle);

                //Temporary list to store arc points of negative arc
                ArrayList negativeArc = new ArrayList();
                for (int index = 0; index < pointCount-1; index++)
                {
                    float pointX = (circleCenter.position.x + Mathf.Cos(startAngle + sign * index / degreeFactor) * radius);
                    float pointZ = (circleCenter.position.z + Mathf.Sin(startAngle + sign * index / degreeFactor) * radius);
                    Node lTmp = new Node(new Vector3(pointX, circleCenter.position.y, pointZ));

                    //LEFT TURN
                    //if sign is positive add arc points to path
                    if (sign > 0 && angle > 0)
                        smoothList.Add(lTmp);
                    else if (sign < 0 && angle < 0)
                        negativeArc.Add(lTmp);
                    //RIGHT TURN
                    else if (sign > 0 && angle < 0)
                        negativeArc.Add(lTmp);
                    else if (sign < 0 && angle > 0)
                        smoothList.Add(lTmp);
                }

                //Add negative arc in reverse order to keep path nodes in right order
                if ((sign < 0 && angle < 0) || (sign > 0 && angle < 0))
                {
                    negativeArc.Reverse();
                    for (int index = 0; index < negativeArc.Count; index++)
                    {
                        smoothList.Add(negativeArc[index]);
                    }
                }

                //Next two points are replaced with arc, skip them (j and k)...
                i = i + 2;
            }
        }

        return smoothList;
    }

    private static double RadianToDegree(float angle)
    {
        return angle * (180.0 / Mathf.PI);
    }

    private static float GetLength(float dx, float dz)
    {
        return Mathf.Sqrt(dx*dx + dz*dz);
    }

    private static Node GetProportionPoint(Node aAngularPoint, float aSegment, float aLength, float aDx, float aDz)
    {
        float factor = aSegment / aLength;

        return new Node(new Vector3(aAngularPoint.position.x - aDx * factor, aAngularPoint.position.y, aAngularPoint.position.z - aDz * factor));
    }
}
