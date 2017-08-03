using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CCTVCamera : MonoBehaviour {

    public GameObject[] NPCObjects;
    //If spotted by camera raise alarm permanently
    public static bool alert = false;
    public bool playerInSight = false;
    public float alertTime = 15.0f;
    private MeshRenderer Text;
    public GameObject PlayerSilouete;

	// Use this for initialization
	void Start () 
    {
        if (NPCObjects.Length == 0)
            Debug.LogError("NPCObjects are not assigned!");

        MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
        Text = children[0].GetComponent<MeshRenderer>();
        if (Text == null)
            Debug.LogError("3D Text object not found");

        if (PlayerSilouete == null)
            Debug.LogError("Player Silouete object not assigned!");

        //reset alert state on beggining
        alert = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if(other.tag.Equals("Player") && alert == false)
        {
            //Debug.Log("Player spotted!");

            //Set new end points for all A STAR NPCs 
            for(int i=0; i<NPCObjects.Length; i++)
            {
                
                NPCObjects[i].GetComponent<ExecuteAStar>().objEndCube.transform.position = new Vector3(other.transform.position.x,
                                                                                                       0.5f,
                                                                                                       other.transform.position.z);

                //Execute A Star once
                NPCObjects[i].GetComponent<ExecuteAStar>().enabled = true;
                //Enable path following
                NPCObjects[i].GetComponent<NPCFollowingAStarPath>().enabled = true;
            }

            //Raise alarm
            alert = true;

            playerInSight = true;

            //Enable Text
            Text.enabled = true;
            StartCoroutine(TurnOffText(alertTime));
            GameObject.Find("AlertIndicatorText").GetComponent<Text>().text = "Guards alerted";
            GameObject.Find("AlertIndicatorText").GetComponent<Text>().color = Color.red;

            //Enable player silouete and place it on player position
            PlayerSilouete.SetActive(true);
            PlayerSilouete.transform.position = new Vector3(other.transform.position.x,
                                                            0.5f,
                                                           other.transform.position.z);

        }

    }

    IEnumerator TurnOffText(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //disable text
        Text.enabled = false;
        //reset alarm state to enable cameras detection..
        alert = false;
        //change ui indicator
        GameObject.Find("AlertIndicatorText").GetComponent<Text>().text = "Guards idle";
        GameObject.Find("AlertIndicatorText").GetComponent<Text>().color = Color.white;
        //Disable Player silouete
        PlayerSilouete.SetActive(false);
    }
}
