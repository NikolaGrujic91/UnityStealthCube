using UnityEngine;
using System.Collections;

/// <summary>
/// Another sense we're going to implement is Touch.cs, which is triggered when the
/// player entity is within a certain area near the AI entity. Our AI character has a box
/// collider component, and its Is Trigger flag is on.
///
/// We need to implement OnTriggerEnter event that will be fired whenever the
/// collider component is collided with another collider component. Since our tank
/// entity also has a collider and rigid body components, collision events will be raised
/// as soon as the colliders of the AI character and player tank are collided.
/// </summary>

public class Sound : Sense {

    public GameObject DetectorText;
    public bool playerInSightSound = false;

    void Start()
    {
        if (DetectorText == null)
            Debug.LogError("Detector Text is not assigned!");
    }

    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.gameObject.GetComponent<Aspect>();
        if (aspect != null)
        { 
            //Check the aspect
            if (aspect.aspectName == aspectName)

                /***************************************************************************************
                 * SOUND
                 * ***********************************************************************************/
                //Check if player is moving
                playerInSightSound = true;
                if(other.GetComponent<CharacterController>().velocity != Vector3.zero)
                {
                    //Debug.Log("Movement Sound detected!");
                    /***********************************************************
                     * ACTIVATE SCRIPT TO FOLLOW PLAYER              *
                     **********************************************************/
                    gameObject.GetComponentInParent<NPCFollowingPath>().enabled = false;
                    gameObject.GetComponentInParent<NPCFollowingPlayer>().enabled = true;

                    /***********************************************************
                     * ACTIVATE DETECTOR TEXT                                  *
                     **********************************************************/
                    DetectorText.GetComponent<MeshRenderer>().enabled = true;
                    
                }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Aspect aspect = other.gameObject.GetComponent<Aspect>();
        if (aspect != null)
        { 
            //Check the aspect
            if (aspect.aspectName == aspectName)
                //Check if player is moving
                playerInSightSound = false;
        }
    }
}
