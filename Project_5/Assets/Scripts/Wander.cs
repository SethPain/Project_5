using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour {
    Vector3 targetPosition; // Current target position
    Vector3 currentPosition; // Object's current position
    Vector3 previousPosition; // Object's position on last update
    Vector3 velocity; // Object's velocity
    Vector3 desiredVel; // The desired velocity
    Vector3 seekForce; // The steering force
    Vector3 randomUnitVector;   // A randon vector from the unit circle
    int count = 0; // A count for when to change targets
    public float Speed = 7f; // Speed by which object wanders

	void Start () {
        currentPosition = transform.position; // Initialize object's current position
        previousPosition = transform.position; // Initialize object's previous position
        randomUnitVector = Random.insideUnitSphere; // Initialize randon vector
        targetPosition = new Vector3(currentPosition.x + Random.Range(-3, 10), 1, currentPosition.z + Random.Range(-3, 10)) + new Vector3(randomUnitVector.x * Random.Range(1, 50), 0, randomUnitVector.z * Random.Range(1, 50)); // Initialize target position
    }
	
	// Update is called once per frame
	void Update () {
        // Change targets after a certain number of updates
        if (count == 75)
        {
            randomUnitVector = Random.insideUnitSphere;
            if (randomUnitVector.z < 0)
            {
                // randomUnitVector.z *= -1;
            }
            targetPosition = new Vector3(currentPosition.x + Random.Range(1, 10), 1, currentPosition.z + Random.Range(1, 10)) + new Vector3(randomUnitVector.x * Random.Range(1, 25), 0, randomUnitVector.z * Random.Range(1, 25));
            count = 0;
        }
        count++;

        
        
        
        velocity = currentPosition - previousPosition; // Update object's velocity
        desiredVel = (targetPosition - currentPosition).normalized * Speed * Time.deltaTime; // Calculate the desired velocity
        seekForce = (desiredVel - velocity); // Calculate steering force
        previousPosition = transform.position; // Update object's previous position
        velocity = velocity + seekForce; // Apply steering force to velocity
        transform.position = (transform.position + velocity); // Apply velocity to object
    }

    // Send object back to middle of play area
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Wall")
        {
            transform.position = new Vector3(25, 1, 25);
            
        }
    }
}
