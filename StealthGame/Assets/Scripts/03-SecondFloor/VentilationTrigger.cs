using UnityEngine;
using System.Collections;

public class VentilationTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
            GameObject.Find("UIManager").GetComponent<UIManager>().levelCompleted = true;
    }
}
