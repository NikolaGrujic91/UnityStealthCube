using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FSMTriggerPerspectiveNear : MonoBehaviour {

    public bool playerInSightPerspective = false;
    public GameObject DetectorText;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Check the object and visibility...
        if (other.tag.Equals("Player") && Invisibility.invisible == false)
        {
            //Set ray casting direction...
            Vector3 direction = other.transform.position - transform.position;
            direction = new Vector3(direction.x, -1.8f, direction.z);

            int npcLayer = 1 << 10;//Get NPC layer
            npcLayer = ~npcLayer;//cast ray against all layer except npc layer

            RaycastHit hit;

            // ... and if a raycast towards the player hits something...
            if (Physics.Raycast(transform.position, direction.normalized, out hit, 15.0f, npcLayer))
            {
                Debug.Log("RaycastHit:" + hit.collider.name);
                // ... and if the raycast hits the player...
                if (hit.collider.tag.Equals("Player"))
                {
                    playerInSightPerspective = true;

                    DetectorText.GetComponent<MeshRenderer>().enabled = true;
                    DetectorText.GetComponent<TextMesh>().text = "!";
                    DetectorText.GetComponent<TextMesh>().color = Color.red;
                    GetComponent<AudioSource>().Play();

                    //If player in near perspective switch to END state...
                    transform.parent.gameObject.GetComponent<StealthFSM>().currentState = StealthFSM.FSMState.End;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
            playerInSightPerspective = false;
    }
}
