using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FSMTriggerPerspectiveWide : MonoBehaviour {

    public bool playerInSightPerspective = false;
    public int stateProbability = 50;
    public GameObject[] NPCObjects;
    public GameObject DetectorText;

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
            //Set ray casting direction...
            Vector3 direction = other.transform.position - transform.position;
            direction = new Vector3(direction.x, -1.8f, direction.z);

            int npcLayer = 1 << 10;//Get NPC layer...
            npcLayer = ~npcLayer;//cast ray against all layer except npc layer...

            RaycastHit hit;

            // ... and if a raycast towards the player hits something...
            if (Physics.Raycast(transform.position, direction.normalized, out hit, 15.0f, npcLayer))
            {
                Debug.Log("RaycastHit:" + hit.collider.name);
                // ... and if the raycast hits the player...
                if (hit.collider.tag.Equals("Player"))
                {
                    playerInSightPerspective = true;

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
                        for (int i = 0; i < NPCObjects.Length; i++)
                            NPCObjects[i].GetComponent<StealthFSM>().currentState = StealthFSM.FSMState.Surround;

                    DetectorText.GetComponent<MeshRenderer>().enabled = true;
                    DetectorText.GetComponent<TextMesh>().text = "?";
                    DetectorText.GetComponent<TextMesh>().color = Color.yellow;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag.Equals("Player"))
            playerInSightPerspective = false;
    }
}
