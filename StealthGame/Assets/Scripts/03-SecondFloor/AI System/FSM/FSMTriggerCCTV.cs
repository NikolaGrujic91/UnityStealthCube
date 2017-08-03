using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FSMTriggerCCTV : MonoBehaviour {
    //Add all NPC objects that are to be triggered by this camera...
    public GameObject[] NPCObjects;
    public bool playerInSight = false;
    public float alertTime = 15.0f;
    private MeshRenderer Text;
    public GameObject PlayerSilouete;

    // Use this for initialization
    void Start()
    {
        if (NPCObjects.Length == 0)
            Debug.LogError("NPCObjects are not assigned!");

        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        Text = children[0].GetComponent<MeshRenderer>();
        if (Text == null)
            Debug.LogError("3D Text object not found");

        if (PlayerSilouete == null)
            Debug.LogError("Player Silouete object not assigned!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerInSight = true;

            //Enable player silouete and place it on player position
            PlayerSilouete.SetActive(true);
            PlayerSilouete.transform.position = new Vector3(other.transform.position.x,
                                                            0.5f,
                                                           other.transform.position.z);
            
            //Calculate distance from NPCs to camera, and choose only nearest to check alarm
            float minDistance = Vector3.Distance(PlayerSilouete.transform.position, NPCObjects[0].transform.position);
            int index = 0;
            for (int i = 1; i < NPCObjects.Length; i++)
            {
                if(minDistance > Vector3.Distance(PlayerSilouete.transform.position, NPCObjects[i].transform.position))
                {
                    minDistance = Vector3.Distance(PlayerSilouete.transform.position, NPCObjects[i].transform.position);
                    index = i;
                }
            }

            //Set new states for all NPCs 
            for (int i = 0; i < NPCObjects.Length; i++)
            {
                //Nearest switches to ALERT state...
                if(i == index)
                {
                    Debug.Log("Nearest " + index + " Distance:" + minDistance);
                    NPCObjects[i].GetComponent<StealthFSM>().currentState = StealthFSM.FSMState.Alert;
                    NPCObjects[i].GetComponent<StealthFSM>().currentWaypoint = PlayerSilouete;
                }
                //Other switch to AWARE state...
                else
                    NPCObjects[i].GetComponent<StealthFSM>().currentState = StealthFSM.FSMState.Aware;             

                //Set perspective and sound areas bigger for all NPCs...
                NPCObjects[i].GetComponent<StealthFSM>().SoundArea.transform.localScale = new Vector3(24.0f, 1.0f, 24.0f);
                NPCObjects[i].GetComponent<StealthFSM>().PerspectiveWide.SetActive(true);
                NPCObjects[i].GetComponent<StealthFSM>().PerspectiveWide.transform.localScale = new Vector3(2.0f, 1.0f, 3.2f);
            }


            //Enable GUI Text that NPC are alerted...
            Text.enabled = true;
            StartCoroutine(TurnOffText(alertTime));
            GameObject.Find("AlertIndicatorText").GetComponent<Text>().text = "Guards alerted";
            GameObject.Find("AlertIndicatorText").GetComponent<Text>().color = Color.red;
        }
    }

    IEnumerator TurnOffText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //disable text
        Text.enabled = false;
        //change ui indicator
        GameObject.Find("AlertIndicatorText").GetComponent<Text>().text = "Guards idle";
        GameObject.Find("AlertIndicatorText").GetComponent<Text>().color = Color.white;
        //Disable Player silouete
        PlayerSilouete.SetActive(false);
    }
}
