using UnityEngine;
using System.Collections;

public class GrassMovementSound : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
            Debug.LogError("Player not found!");
    }
	
	void Update () 
    {
        if (player.GetComponent<CharacterController>().velocity != Vector3.zero && Invisibility.invisible == true && !GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Play();
        else if (player.GetComponent<CharacterController>().velocity == Vector3.zero && Invisibility.invisible == true && GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Stop();
	}
}
