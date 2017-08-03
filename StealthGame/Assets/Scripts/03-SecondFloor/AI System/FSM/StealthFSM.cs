using UnityEngine;
using System.Collections;

public class StealthFSM : MonoBehaviour {

    public GameObject[] NPCList;
    public GameObject[] IdlePatrolPoints;
    public int currentIndex = 0;
    public GameObject currentWaypoint;
    public NavMeshAgent navMeshAgent;
    public GameObject PerspectiveWide;
    public GameObject PerspectiveNear;
    public GameObject SoundArea;
    public GameObject DetectorText;
    public GameObject SurroundWaypoint;

    public int WaypointsRangeBottom = 0;
    public int WaypointsRangeTop = 16;
    public enum FSMState
    {
        None,
        Idle,
        Alert,
        Aware,
        Surround,
        Chase,
        End
    }

    public FSMState currentState;

	void Start () 
    {
	    //Default state
        currentState = FSMState.Idle;

        //Get nav mesh agent
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
            Debug.LogError("NavMeshAgent not found!");

        if (PerspectiveWide == null)
            Debug.LogError("Perspective Wide not found!");

        if (PerspectiveNear == null)
            Debug.LogError("Perspective Near not found!");

        if (SoundArea == null)
            Debug.LogError("Sound Area not found!");
	}	

	void Update () 
    {
	    switch(currentState)
        {
            case FSMState.Alert: UpdateAlertState(); break;
            case FSMState.Aware: UpdateAwareState(); break;
            case FSMState.Chase: UpdateChaseState(); break;
            case FSMState.End: UpdateEndState(); break;
            case FSMState.Idle: UpdateIdleState(); break;
            case FSMState.Surround: UpdateSurroundState(); break;
        }
	}

    /************************************************************
     * IDLE
     * NPC patrolling on predefined path. Sensor areas are normal.
     * 
     * 1. If player is seen/heard switch to state END
     * 2. If player is seen by camera switch to state ALERT
     * **********************************************************/
    void UpdateIdleState()
    {
        //NPC patrolling by randomly choosing one of predefined waypoints
        currentWaypoint = IdlePatrolPoints[currentIndex];
        navMeshAgent.destination = currentWaypoint.transform.position;

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < navMeshAgent.stoppingDistance)
            currentIndex = Random.Range(WaypointsRangeBottom, WaypointsRangeTop);
    }

    /************************************************************
     * ALERT
     * NPC going to position where camera has spotted player. Sensor areas are wider.
     * 
     * 1. If player is spotted in wide perspective or sound detection zone, by probability
     *    is choosing to go to SURROUND or CHASE state.
     * 2. If NPC reach position where camera has spotted player, it's going back to patrolling,
     *    but detection areas are staying wide, switches to AWARE state.
     * 3. If player is seen in close perspective go to END state.
     * **********************************************************/
    void UpdateAlertState()
    {
        navMeshAgent.destination = currentWaypoint.transform.position;

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < navMeshAgent.stoppingDistance)
            currentState = FSMState.Aware;
    }

    /************************************************************
     * AWARE
     * NPC patrolling on predefined path. Sensor areas are wider.
     * 
     * 1. If player is spotted in wide perspective or sound detection zone, by probability
     *    is choosing to go to SURROUND or CHASE state.
     * 2. If player is seen in close perspective go to END state.
     * **********************************************************/
    void UpdateAwareState()
    {
        //NPC patrolling by randomly choosing one of predefined waypoints
        currentWaypoint = IdlePatrolPoints[currentIndex];
        navMeshAgent.destination = currentWaypoint.transform.position;

        if (Vector3.Distance(transform.position, currentWaypoint.transform.position) < navMeshAgent.stoppingDistance)
            currentIndex = Random.Range(WaypointsRangeBottom, WaypointsRangeTop);
    }

    /************************************************************
     * CHASE
     * NPC is going towards player position alone.
     * 
     * 1. If player is spotted in wide perspective or sound detection zone,
     *    chasing continues
     * 2. If player is seen in close perspective go to END state.
     * 3. If player reaches safe zone, and gets invisible, goes to AWARE state.
     * **********************************************************/
    void UpdateChaseState()
    {
        if (Invisibility.invisible == false)
            navMeshAgent.destination = currentWaypoint.transform.position;
        else
            currentState = FSMState.Aware;
    }

    /************************************************************
     * SURROUND
     * NPCs are trying to surround player together. Every NPC has its own surround 
     * waypoint which determines from which way it should approach player.
     * 
     * 1. If player is spotted in wide perspective or sound detection zone,
     *    surroundung continues
     * 2. If player is seen in close perspective go to END state.
     * 3. If player reaches safe zone, and gets invisible, goes to AWARE state.
     * **********************************************************/
    void UpdateSurroundState()
    {
        currentWaypoint = SurroundWaypoint;

        if (Invisibility.invisible == false)
            navMeshAgent.destination = currentWaypoint.transform.position;
        else
            currentState = FSMState.Aware;
    }

    /************************************************************
     * END
     * NPCs reached player with near perspective. It calls for game over.
     * **********************************************************/
    void UpdateEndState()
    {
        DetectorText.GetComponent<MeshRenderer>().enabled = true;//activate detector text
        GameObject.Find("UIManager").GetComponent<UIManager>().gameOver = true;//set game over UI
    }
}
