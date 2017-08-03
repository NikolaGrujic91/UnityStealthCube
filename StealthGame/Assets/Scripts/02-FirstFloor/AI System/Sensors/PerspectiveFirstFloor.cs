using UnityEngine;
using System.Collections;

public class PerspectiveFirstFloor : Sense {

    public bool playerInSightPerspective = false;
    public GameObject DetectorText;

    void Start()
    {
        if (DetectorText == null)
            Debug.LogError("Detector Text is not assigned!");
    }

    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.gameObject.GetComponent<Aspect>();
        if (aspect != null)
        {
            //Check the aspect
            if (aspect.aspectName == aspectName && Invisibility.invisible == false)
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
                        DetectorText.GetComponent<MeshRenderer>().enabled = true;//activate detector text
                        GameObject.Find("UIManager").GetComponent<UIManager>().gameOver = true;//set game over UI
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Aspect aspect = other.gameObject.GetComponent<Aspect>();
        if (aspect != null)
        {
            //Check the aspect
            if (aspect.aspectName == aspectName)
                playerInSightPerspective = false;
        }
    }
}
