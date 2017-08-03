using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HatchText : MonoBehaviour {

    public bool TextEnabled = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Enable Text
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "press [e]  to  open hatch";
            TextEnabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            //Disable text
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            TextEnabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TextEnabled)
        {
            GetComponentInParent<Animation>().Play();
            GameObject.Find("ActionIndicatorText").GetComponent<Text>().text = "";
            this.gameObject.SetActive(false);
        }
    }

}
