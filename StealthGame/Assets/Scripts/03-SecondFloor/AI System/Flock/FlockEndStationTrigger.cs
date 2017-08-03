using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlockEndStationTrigger : MonoBehaviour {

    public GameObject FlockEndStation;
    public GameObject FlockController;
    public bool TriggerEntered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            TriggerEntered = true;
            FlockController.GetComponent<FlockController>().target = FlockEndStation.transform;
            Invisibility.invisible = false;
            GameObject.Find("VisibilityIndicatorText").GetComponent<Text>().text = "visible";
        }
    }
}
