using UnityEngine;
using System.Collections;

public class FSMTriggerMotionDetection : MonoBehaviour {

    public int stateProbability = 50;
    public GameObject[] NPCObjects;
    public bool playerInSightMotion = false;

    void Start()
    {
        if (NPCObjects.Length == 0)
            Debug.LogError("NPCObjects are not assigned!");
    }

    void OnTriggerEnter(Collider other)
    {
        //Check the object and visibility...
        if (other.tag.Equals("Player") && Invisibility.invisible == false)
        {
            playerInSightMotion = true;
            
            //Check if player is moving...
            if (other.GetComponent<Rigidbody>().velocity != Vector3.zero)
            {
                Debug.Log("Velocity:" + other.GetComponent<Rigidbody>().velocity + " Movement Detected!");
                //Choose randomly to go to CHASE/SURROUND state...
                int state = Random.Range(0, 100);

                //Go to CHASE...
                if (state < stateProbability)
                {
                    transform.parent.gameObject.GetComponent<StealthFSM>().currentState = StealthFSM.FSMState.Chase;
                    transform.parent.gameObject.GetComponent<StealthFSM>().currentWaypoint = other.gameObject;
                }
                //Switch all to SURROUND state...
                else
                {
                    for (int i = 0; i < NPCObjects.Length; i++)
                    {
                        NPCObjects[i].GetComponent<StealthFSM>().currentState = StealthFSM.FSMState.Surround;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            playerInSightMotion = false;
    }
}
