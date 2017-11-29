using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {

    GameObject target;              // This will be the target for the seeking behavior
    Vector3 currentPosition;        // This is the fleeing object's current position
    Vector3 previousPosition;       // This is the fleeing object's previous position, before the last change in velocity
    Vector3 targetPosition;         // This is the current position of the target
    Vector3 velocity;               // This is the fleeing object's velocity
    Vector3 desiredVel;             // This is the fleeing object's desired velocity (the vector pointing towards the target)
    Vector3 seekForce;              // This is the difference between the desired velocity and current velocity (the steering force, the vector that needs to be applied to the seeking object with its current velocity in order to reach the target)
    public float Speed = 7f;        // This determines how fast the fleeing object moves
    public float SteeringLimit = 0.01f; // This limits the rate at which the velocity changes
    public float WallBounce = 1.5f; // This force keeps fleeing objects from leaving the play area


    void Awake()
    {
        target = GameObject.Find("Threat"); // Find the target in the scene
        previousPosition = transform.position;  // Initialize the fleeing object's previous position
        currentPosition = transform.position;   // Initialize the fleeing object's current position

    }


    void Update()
    {
        
        targetPosition = target.transform.position; // Update the target's position
        currentPosition = transform.position;       // Update the fleeing object's position
        velocity = currentPosition - previousPosition;  // Calculate the fleeing object's current velocity
        desiredVel = (currentPosition - targetPosition).normalized * Speed * Time.deltaTime;    // Calculate the desired velocity
        seekForce = (desiredVel - velocity);    // Calculate the steering force
        seekForce = new Vector3(seekForce.x * SteeringLimit, seekForce.y, seekForce.z * SteeringLimit); // Limit the rate at which the fleeing force can change the fleeing object's velocity
        previousPosition = transform.position; // Prepare the fleeing object's previous position for the next call to Update()
        velocity = velocity + seekForce;    // Calculate the fleeing object's new velocity
        transform.position = (transform.position + velocity);   // Apply the velocity to the fleeing object

        /* Would just negating the steering force somehow turn seek into flee? Idk
        */
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            transform.position = transform.position - velocity * WallBounce;
        }

    }
}
