using UnityEngine;
using System.Collections;

public class Objective : MonoBehaviour {

    public float fadeOutTime = 5.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(FadeOut());
	}

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeOutTime);
        GetComponent<MeshRenderer>().enabled = false;
    }
}
