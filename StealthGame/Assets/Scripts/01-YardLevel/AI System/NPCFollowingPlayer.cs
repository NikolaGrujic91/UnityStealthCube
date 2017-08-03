using UnityEngine;
using System.Collections;

/// <summary>
/// Follows player if player is within the field of view in Perspective sense
/// </summary>
/// 
public class NPCFollowingPlayer : MonoBehaviour {
    public float speed = 20.0f;
    public float mass = 5.0f;
    public float force = 50.0f;
    public float minimumDistToAvoid = 20.0f;
    public GameObject targetObject;
    public bool enabled = false;

    //Actual speed of the vehicle
    private float curSpeed;
    public Vector3 targetPoint;

	void Start () 
    {
        if (targetObject == null)
            Debug.LogError("Assign target object!");

        mass = 5.0f;
        targetPoint = Vector3.zero;
	}
	
	void Update () 
    {
        if (enabled)
        {
            //Assign player position as target point
            targetPoint = new Vector3(targetObject.transform.position.x,
                                          targetObject.transform.position.y,
                                          targetObject.transform.position.z);

            //Directional vector to the target position
            Vector3 dir = (targetPoint - transform.position);
            dir.Normalize();

            //Dont move the vehicle when the target point is reached
            if (Vector3.Distance(targetPoint, transform.position) < 3.0f)
            {
                //Set game over UI
                GameObject.Find("UIManager").GetComponent<UIManager>().gameOver = true;
                return;
            }

            //Assign the speed with delta time
            curSpeed = speed * Time.deltaTime;

            //Rotate the vehicle to its target directional vector
            var rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5.0f * Time.deltaTime);

            //Move the vehicle towards
            transform.position += transform.forward * curSpeed;
        }
	}
}
