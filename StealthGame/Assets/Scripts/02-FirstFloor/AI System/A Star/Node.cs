using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// The Node class has properties, such as the cost values (G and H), flags to mark whether
/// it is an obstacle, its positions and parent node. The nodeTotalCost is G, which is the
/// movement cost value from starting node to this node so far and the estimatedCost
/// is H, which is total estimated cost from this node to the target goal node. We also have
/// two simple constructor methods and a wrapper method to set whether this node is an
/// obstacle.
/// </summary>
public class Node : IComparable{
    public float nodeTotalCost;
    public float estimatedCost;
    public bool bObstacle;
    public Node parent;
    public Vector3 position;

    public Node()
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
    }

    public Node(Vector3 pos)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.bObstacle = false;
        this.parent = null;
        this.position = pos;
    }

    public void MarkAsObstacle()
    {
        this.bObstacle = true;
    }

    /// <summary>
    /// we implement this method to sort the node objects based on our estimatedCost value
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo(object obj)
    {
        Node node = (Node)obj;

        //Negative value means object comes before this in the sort order
        if (this.estimatedCost < node.estimatedCost)
            return -1;

        //Positive value means object comes after this in the sort order
        if (this.estimatedCost > node.estimatedCost)
            return 1;

        return 0;
    }

}
