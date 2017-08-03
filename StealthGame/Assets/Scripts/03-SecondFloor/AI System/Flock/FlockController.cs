using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// FlockController is a simple behavior to generate the boids at runtime and update the
/// center of the flock as well as the average velocity of the flock.
/// </summary>
public class FlockController : MonoBehaviour {

    public float minVelocity = 1; // Min Velocity
    public float maxVelocity = 8; // Max Velocity
    public int flockSize = 20; // Number of flocks in the group

    // How far the boids should stick to the center ( the more weight stick closer to the center)
    public float centerWeight = 1;

    public float velocityWeight = 1; // Alignment behavior

    // How far each boid should be separated within the flock
    public float separationWeight = 1;

    // How close each boid should follow to the leader (the more weight the closer follow)
    public float followWeight = 1;

    // Additional random noise
    public float randomizeWeight = 1;

    public Flock prefab;
    public Transform target;

    // Center position of the flock in the group
    internal Vector3 flockCenter;
    internal Vector3 flockVelocity; //Average Velocitys

    public ArrayList flockList = new ArrayList();

    /// <summary>
    /// We declare all the properties to implement the flocking algorithm and then start with
    /// the generation of the boid objects based on the flock size input. We set up the controller
    /// class and parent transform object like we did last time. Then, we add the created boid
    /// object in our ArrayList function. The target variable accepts an entity to be used as a
    /// moving leader. We'll create a sphere entity as a moving target leader for our flock.
    /// </summary>
	void Start () 
    {
        for (int i = 0; i < flockSize; i++)
        {
            Flock flock = Instantiate(prefab, target.position, target.rotation) as Flock;
            flock.transform.parent = transform;
            flock.controller = this;
            flockList.Add(flock);    
        }
	}
	
    /// <summary>
    /// In our Update() method, we keep updating the average center and velocity of the
    /// flock. Those are the values referenced from our boid object, and they are used to
    /// adjust the cohesion and alignment properties with the controller.
    /// </summary>
	void Update () 
    {
	    //Calculate the Center and Velocity of the whole flock group
        Vector3 center = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        foreach (Flock flock in flockList)
        {
            center += flock.transform.localPosition;
            velocity += flock.GetComponent<Rigidbody>().velocity;
        }

        flockCenter = center / flockSize;
        flockVelocity = velocity / flockSize;
	}
}
