using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NightVisionController : MonoBehaviour {

    public GameObject normalVisionCamera;
    public GameObject nightVisionCamera;

	// Use this for initialization
	void Start () 
    {
        if (normalVisionCamera == null)
            Debug.LogError("Normal Vision camera not assigned!");
        if(nightVisionCamera == null)
            Debug.LogError("Night Vision camera not assigned!");
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(Input.GetKeyDown(KeyCode.N))
        {
            // Keep camera position and rotation
            if (normalVisionCamera.activeSelf == true)
            {
                nightVisionCamera.transform.position = new Vector3(normalVisionCamera.transform.position.x,
                                                              normalVisionCamera.transform.position.y,
                                                              normalVisionCamera.transform.position.z);

                nightVisionCamera.transform.eulerAngles = new Vector3(normalVisionCamera.transform.rotation.eulerAngles.x,
                                                                 normalVisionCamera.transform.rotation.eulerAngles.y,
                                                                 normalVisionCamera.transform.rotation.eulerAngles.z);

                // Enable/Disable cameras
                normalVisionCamera.SetActive(false); // Normal camera Off
                nightVisionCamera.SetActive(true); // Night vision On
                GameObject.Find("NightVisionIndicatorText").GetComponent<Text>().text = "[N] NIGHT VISION ON";
            }
            else if (normalVisionCamera.activeSelf == false)
            {
                //Keep camera position and rotation
                normalVisionCamera.transform.position = new Vector3(nightVisionCamera.transform.position.x,
                                                                    nightVisionCamera.transform.position.y,
                                                                    nightVisionCamera.transform.position.z);

                normalVisionCamera.transform.eulerAngles = new Vector3(nightVisionCamera.transform.rotation.eulerAngles.x,
                                                                       nightVisionCamera.transform.rotation.eulerAngles.y,
                                                                       nightVisionCamera.transform.rotation.eulerAngles.z);

                // Enable/Disable cameras
                normalVisionCamera.SetActive(true); // Normal camera On
                nightVisionCamera.SetActive(false); // Night vision Off
                GameObject.Find("NightVisionIndicatorText").GetComponent<Text>().text = "[N] NIGHT VISION OFF";
            }
        }
	}
}
