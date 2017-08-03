using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlockStationTrigger : MonoBehaviour {

    public GameObject Player;
    public GameObject FlockController;
    public bool TriggerEntered = false;

	// Use this for initialization
	void Start () 
    {
        if (Player == null)
            Debug.LogError("Player not assigned!");

        if (FlockController == null)
            Debug.LogError("Flock Controller not assigned!");
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "press [e]  to  blend";
            TriggerEntered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            TriggerEntered = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TriggerEntered == true)
        {
            FlockController.GetComponent<FlockController>().target = Player.transform;
            Invisibility.invisible = true;
            GameObject.Find("VisibilityIndicatorText").GetComponent<Text>().text = "invisible";
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            this.gameObject.SetActive(false); // Deactivate flock station trigger object
        }
    }

}
