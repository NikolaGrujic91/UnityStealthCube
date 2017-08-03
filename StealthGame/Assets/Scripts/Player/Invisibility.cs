using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Invisibility : MonoBehaviour {

    public static bool invisible = false;

    void Start()
    {
        invisible = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("InvisibilityArea"))
        {
            invisible = true;
            GameObject.Find("VisibilityIndicatorText").GetComponent<Text>().text = "invisible";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("InvisibilityArea"))
        {
            invisible = false;
            GameObject.Find("VisibilityIndicatorText").GetComponent<Text>().text = "visible";
        }
    }
}
