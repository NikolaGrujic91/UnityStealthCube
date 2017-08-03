using UnityEngine;
using System.Collections;

public class MrHandyMovement : MonoBehaviour {
    public GameObject[] IdlePatrolPoints;
    public int currentIndex = 0;
    public GameObject currentWaypoint;
    public NavMeshAgent navMeshAgent;
    public int WaypointsRangeBottom = 0;
    public int WaypointsRangeTop = 16;

	// Use this for initialization
	void Start () 
    {
        // Get nav mesh agent
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.LogError("NavMeshAgent not found!");
	}
	
	// Update is called once per frame
	void Update ()
    {
        // NPC patrolling by random choosing one of predefined waypoints
        currentWaypoint = IdlePatrolPoints[currentIndex];
        navMeshAgent.destination = currentWaypoint.transform.position;

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < navMeshAgent.stoppingDistance)
            currentIndex = Random.Range(WaypointsRangeBottom, WaypointsRangeTop);
	}
}
