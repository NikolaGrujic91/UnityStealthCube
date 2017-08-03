using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Flock : MonoBehaviour {
    internal FlockController controller;


    /// <summary>
    /// In our Update() method, we
    /// calculate the velocity for our boid using the following steer() method and apply
    /// it to its rigid body velocity. Next, we check the current speed of our rigid body
    /// component to verify whether it's in the range of our controller's maximum and
    /// minimum velocity limits. If not, we cap the velocity at the preset range:
    /// </summary>
    void Update()
    {
        if (controller)
        {
            Vector3 relativePos = steer() * Time.deltaTime;

            if (relativePos != Vector3.zero)
                GetComponent<Rigidbody>().velocity = relativePos;
        
            // Enforce minimum and maximum speeds for the boids
            float speed = GetComponent<Rigidbody>().velocity.magnitude;
            if (speed > controller.maxVelocity)
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.maxVelocity;
            else if (speed < controller.minVelocity)
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * controller.minVelocity;
        }
    }


    /// <summary>
    /// The steer() method implements separation, cohesion, alignment, and follows the
    /// leader rules of the flocking algorithm. Then, we sum up all the factors together with
    /// a random weight value. With this Flock script together with rigid body and sphere
    /// collider components, we create a Flock prefab
    /// </summary>
    /// <returns></returns>
    private Vector3 steer()
    { 
        // cohesion
        Vector3 center = controller.flockCenter - transform.localPosition;

        // alignment
        Vector3 velocity = controller.flockVelocity - GetComponent<Rigidbody>().velocity;

        // follow leader
        Vector3 follow = controller.target.localPosition - transform.localPosition;

        Vector3 separation = Vector3.zero;
        
        foreach (Flock flock in controller.flockList)
        {
            if (flock != this)
            {
                Vector3 relativePos = transform.localPosition - flock.transform.localPosition;
                separation += relativePos / (relativePos.sqrMagnitude);
            }
        }

        Vector3 randomize = new Vector3((Random.value * 2) - 1,
                                        (Random.value * 2) - 1,
                                        (Random.value * 2) - 1);

        randomize.Normalize();

        return (controller.centerWeight * center +
                controller.velocityWeight * velocity +
                controller.separationWeight * separation +
                controller.followWeight * follow +
                controller.randomizeWeight * randomize);
    }
}
