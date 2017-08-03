using UnityEngine;
using System.Collections;

public class ObstacleAvoidance : MonoBehaviour {
    public float speed = 20.0f;
    public float mass = 5.0f;
    public float force = 50.0f;
    public float minimumDistToAvoid = 20.0f;
    private float curSpeed;
    private Vector3 targetPoint;
    public Transform Waypoint1;
    public Transform Waypoint2;
    public bool SwitchWaypointsIndicator = true; 

    // Use this for initialization
    void Start()
    {
        mass = 5.0f;

        if (Waypoint1 == null)
            Debug.LogError("Waypoint1 is not assigned!");
        else
            targetPoint = new Vector3(Waypoint1.position.x, Waypoint1.position.y, Waypoint1.position.z);

        if (Waypoint2 == null)
            Debug.LogError("Waypoint2 is not assigned!");
    }

    // Update is called once per frame
    void Update()
    {
        // Directional vector to the target position
        Vector3 dir = (targetPoint - transform.position);
        dir.Normalize();

        // Apply obstacle avoidance
        AvoidObstacles(ref dir);

        // Don't move the vehicle when the target point is reached
        if (Vector3.Distance(targetPoint, transform.position) < 3.0f)
            SwitchWaypoints();

        // Assign the speed with delta time
        curSpeed = speed * Time.deltaTime;

        // Rotate the vehicle to its target directional vector
        var rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 5.0f * Time.deltaTime);

        // Move the vehicle towards
        transform.position += transform.forward * curSpeed;
    }

    //Calculate the new directional vector to avoid the obstacle
    public void AvoidObstacles(ref Vector3 dir)
    {
        RaycastHit hit;
        // Only detect layer 8 (Obstacles)
        int layerMask = 1 << 8;
        // Check that the vehicle hit with the obstacles within
        // it's minimum distance to avoid
        if (Physics.Raycast(transform.position, transform.forward, out hit, minimumDistToAvoid, layerMask))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red);
            // Get the normal of the hit point to calculate the
            // new direction
            Vector3 hitNormal = hit.normal;
            hitNormal.y = 0.0f; //Don't want to move in Y-Space

            // Get the new directional vector by adding force to
            // vehicle's current forward vector
            dir = transform.forward + hitNormal * force;
        }
        else
            Debug.DrawRay(transform.position, transform.forward, Color.green);
    }

    void SwitchWaypoints()
    {
        if (SwitchWaypointsIndicator)
        {
            targetPoint = new Vector3(Waypoint2.position.x, Waypoint2.position.y, Waypoint2.position.z);
            SwitchWaypointsIndicator = false;
        }
        else
        {
            targetPoint = new Vector3(Waypoint1.position.x, Waypoint1.position.y, Waypoint1.position.z);
            SwitchWaypointsIndicator = true;
        }
    }
}
