using UnityEngine;
using System.Collections;

public class ExecuteAStar : MonoBehaviour {
    private Transform startPos, endPos;
    public Node startNode { get; set; }
    public Node goalNode { get; set; }
    public ArrayList pathArray;
    public GameObject objStartCube, objEndCube;
    
    // Interval time between pathfinding
    public float intervalTime = 1.0f;

    public bool enabled = false;

	void Start ()
    {
        if (objStartCube == null)
            Debug.Log("Start not found!");
        else
            Debug.Log("Start found!");


        if (objEndCube == null)
            Debug.Log("End not found!");
        else
            Debug.Log("End found!");

        pathArray = new ArrayList();
        
        FindPath();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (enabled)
            FindPath();
	}

    void FindPath()
    {
        startPos = objStartCube.transform;
        endPos = objEndCube.transform;

        startNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(startPos.position)));

        goalNode = new Node(GridManager.instance.GetGridCellCenter(GridManager.instance.GetGridIndex(endPos.position)));

        pathArray = AStar.FindPath(startNode, goalNode);

        //Convert A Star path to Path class array 
        Vector3[] pointA = new Vector3[pathArray.Count];
        int i = 0;
        foreach (Node node in pathArray)
        {
            //Fixed Y position to prevent NPC cube from dropping through floor
            pointA[i] = new Vector3(node.position.x, 0.5f, node.position.z);
            i++;
        }
        GetComponent<Path>().pointA = pointA;

        //Disable pathfinding, execute it only once
        //Prevents constant execution, optimizes performanse
        enabled = false;
    }

    void OnDrawGizmos()
    {
        if (pathArray == null)
            return;

        if(pathArray.Count > 0)
        {
            int index = 1;
            foreach(Node node in pathArray)
            {
                if(index < pathArray.Count)
                {
                    Node nextNode = (Node)pathArray[index];
                    Debug.DrawLine(node.position, nextNode.position, Color.red);
                    index++;
                }
            }
        }
    }

    public void SwitchEndpoints()
    {
        GameObject temp = objStartCube;
        objStartCube = objEndCube;
        objEndCube = temp;

        GetComponent<Path>().Reverse();
    }
}
