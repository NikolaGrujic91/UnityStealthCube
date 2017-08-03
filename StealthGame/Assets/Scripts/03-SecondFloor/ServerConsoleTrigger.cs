using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ServerConsoleTrigger : MonoBehaviour {

    public bool TextEnabled = false;
    public GameObject Smoke;
    public GameObject ObjectiveArrow1a;
    public GameObject ObjectiveArrow1b;
    public GameObject ObjectiveArrow2;
    public GameObject VentilationTrigger;

    // Use this for initialization
    void Start () {
        Smoke.SetActive(false);

        if (ObjectiveArrow1a == null)
            Debug.Log("ObjectiveArrow1 is not assigned!");

        if (ObjectiveArrow1b == null)
            Debug.Log("ObjectiveArrow1 is not assigned!");

        if (ObjectiveArrow2 == null)
            Debug.Log("ObjectiveArrow2 is not assigned!");

        if (VentilationTrigger == null)
            Debug.Log("VentilationTrigger is not assigned!");
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.E) && TextEnabled)
        {
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            Smoke.SetActive(true);

            // Disable old and activate new objective arrow
            ObjectiveArrow1a.SetActive(false);
            ObjectiveArrow1b.SetActive(false);
            ObjectiveArrow2.SetActive(true);

            // Activate ventilation trigger to finish the level
            VentilationTrigger.SetActive(true);

            // Set new objective text
            GameObject.Find("ObjectiveIndicatorText").GetComponent<Text>().text = "Objective: escape from server room";

            this.gameObject.SetActive(false);

        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            // Enable Text
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "press  [e]  to  disable server";
            TextEnabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            // Disable text
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            TextEnabled = false;
        }
    }
}
