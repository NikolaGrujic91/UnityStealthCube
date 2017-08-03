using UnityEngine;
using System.Collections;

public class StairsText : MonoBehaviour {

    public bool TextEnabled = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
            children[1].GetComponent<MeshRenderer>().enabled = true;
            TextEnabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
            children[1].GetComponent<MeshRenderer>().enabled = false;
            TextEnabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TextEnabled)
        {
            GameObject.Find("UIManager").GetComponent<UIManager>().levelCompleted = true;
        }

    }
}
