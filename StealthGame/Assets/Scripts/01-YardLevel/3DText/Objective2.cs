using UnityEngine;
using System.Collections;

public class Objective2 : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            MeshRenderer[] children = GetComponentsInChildren<MeshRenderer>();
            children[1].GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
