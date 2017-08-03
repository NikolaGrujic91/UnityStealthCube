using UnityEngine;
using System.Collections;

public class NPCFollowingPath : MonoBehaviour {
    public GameObject goPath;
    private Path path;
    public float speed = 20.0f;
    public float mass = 5.0f;
    public bool isLooping = true;
    public bool enabled = true;

    //Actual speed of the vehicle
    private float curSpeed;
    public int curPathIndex;
    private float pathLength;
    private Vector3 targetPoint;

    Vector3 velocity;

	void Start () 
    {
        path = goPath.GetComponent<Path>();
        pathLength = path.Length;
        curPathIndex = 0;

        //get the current velocity of the vehicle
        velocity = transform.forward;    
	}
	
    /// <summary>
    /// In the Update method, we check whether our entity has reached a particular
    /// waypoint by calculating the distance between its current position and the path's
    /// radius range. If it's in the range, we just increase the index to look it up from the
    /// waypoints array. If it's the last waypoint, we check if the isLooping flag is set. If it
    /// is set, then we set the target to the starting waypoint. Otherwise, we just stop at that
    /// point.
    /// </summary>
	void Update () 
    {
        if (enabled == true)
        {
            //Unify the speed
            curSpeed = speed * Time.deltaTime;

            targetPoint = path.GetPoint(curPathIndex);

            //If reach the radius within the path then move to next point in the path
            if (Vector3.Distance(transform.position, targetPoint) < path.Radius)
            {
                //Dont move the vehicle if path is finished
                if (curPathIndex < pathLength - 1)
                    curPathIndex++;
                else if (isLooping)
                    curPathIndex = 0;
                else
                    return;
            }

            //Move the vehicle until the end point is reached in the path
            if (curPathIndex >= pathLength) return;

            //Calculate the next velocity towards the path
            if (curPathIndex >= pathLength - 1 && !isLooping)
                velocity += Steer(targetPoint, true);
            else
                velocity += Steer(targetPoint);

            //Move the vehicle according to the velocity
            transform.position += velocity;
            //Rotate the vehicle towards the desired Velocity
            transform.rotation = Quaternion.LookRotation(velocity);
        }
	}

    /// <summary>
    /// The Steer method takes the parameter; target Vector3 position to move, whether
    /// this is the final waypoint in the path. The first thing we do is calculate the remaining
    /// distance from the current position to the target position. The target position vector
    /// minus the current position vector gives a vector towards the target position vector.
    /// The magnitude of this vector is the remaining distance. We then normalize this
    /// vector just to preserve the direction property. Now, if this is the final waypoint,
    /// and the distance is less than 10 of a number we just decided to use, we slow down
    /// the velocity gradually according to the remaining distance to our point until the
    /// velocity finally becomes zero. Otherwise, we just update the target velocity with
    /// the specified speed value. By subtracting the current velocity vector from this target
    /// velocity vector, we can calculate the new steering vector. Then by dividing this
    // vector with the mass value of our entity, we get the acceleration.
    /// </summary>
    /// <param name="target"></param>
    /// <param name="bFinalPoint"></param>
    /// <returns></returns>
    public Vector3 Steer(Vector3 target, bool bFinalPoint = false)
    { 
        //Calculate the directional vector from the current position towards the target point
        Vector3 desiredVelocity = (target - transform.position);
        float dist = desiredVelocity.magnitude;
    
        //Normalise the desired velocity
        desiredVelocity.Normalize();
    
        //Calculate the velocity according to speed
        if (bFinalPoint && dist < 10.0f)
            desiredVelocity *= (curSpeed * (dist / 10.0f));
        else
            desiredVelocity *= curSpeed;
    
        //Calculate the force vector
        Vector3 steeringForce = desiredVelocity - velocity;
        Vector3 acceleration = steeringForce / mass;

        return acceleration;
    }
}
