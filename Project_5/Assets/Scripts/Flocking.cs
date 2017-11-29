using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Flocking : MonoBehaviour {
    GameObject agent; // Boid that script is on
    Vector3 position; // Object's current position
    Vector3 prevPos;    // Object's position on last update
    GameObject[] agentArray = new GameObject[6];    // Boid array
    Vector3[] prevPosArray = new Vector3[6];        // Boids' positions on last update
    float SteeringLimit = 0.01f; // Steering limit for changing velocity
    public float Speed = 2.5f; // Speed at which boids commit flocking behaviors
    

    void Awake () {
        
    }

	// Use this for initialization
	void Start () {
        agent = this.gameObject; // Initialize the attached agent/boid
        position = transform.position; // Initialize its current position
        prevPos = transform.position; // Initialize its previous position
        
        // Assign boids to boid array
        agentArray[0] = GameObject.Find("Boid 0");
        agentArray[1] = GameObject.Find("Boid 1");
        agentArray[2] = GameObject.Find("Boid 2");
        agentArray[3] = GameObject.Find("Boid 3");
        agentArray[4] = GameObject.Find("Boid 4");
        agentArray[5] = GameObject.Find("Boid 5");

        // Initialize boids' previous positions
        for (int i = 0; i < 6; i++)
        {
            prevPosArray[i] = agentArray[i].transform.position;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject boid; // Use boid to refer to boids throughout the the for loops
        Vector3 desiredVel = new Vector3(0,0,0); // Initialize desired velocity
        Vector3 seekForce = new Vector3(0,0,0); // Initialize 


        // Alignment Behavior
        int neighborCount = 0; // Number of neighbors found
        Vector3 alignVector = new Vector3(0, 0, 0); // Initialize vector for alignment behavior

        // Sum up boid velocties and count neighbors
        for (int i = 0; i < 6; i++)
        {
            boid = agentArray[i];
            if (boid != agent)
            {
                if ((transform.position - boid.transform.position).magnitude < 100)
                {
                    desiredVel += (boid.transform.position - prevPosArray[i]);
                    neighborCount++;
                }
            }
        }
        
        // Find average velocity (Desired velocity)
        if (neighborCount != 0)
        {
            desiredVel.x /= neighborCount;
            desiredVel.z /= neighborCount;
            desiredVel = desiredVel.normalized * Speed * Time.deltaTime;
        }
        seekForce = desiredVel - (agent.transform.position - prevPos); // Calculate steering force
        seekForce = new Vector3(seekForce.x * SteeringLimit, seekForce.y, seekForce.z * SteeringLimit); // Limit steering
        alignVector += seekForce; // Add steering force to alignment vector
        

        // Cohesion behavior
        neighborCount = 0;
        Vector3 cohVector = new Vector3(0, 0, 0);
        desiredVel = new Vector3(0, 0, 0);
        seekForce = new Vector3(0, 0, 0);
        
        // Sum up boid positions
        for (int i = 0; i < 6; i++)
        {
            boid = agentArray[i];
            if (boid != agent)
            {
                if ((transform.position - boid.transform.position).magnitude > 5)
                {
                    //cohVector.x += boid.transform.position.x;
                    // cohVector.z += boid.transform.position.z;
                    cohVector += boid.transform.position;
                    neighborCount++;
                }
            }

        }

        // Find Center of Mass and Cohesion Vector
        if (neighborCount != 0)
        {
            cohVector.x /= neighborCount;
            cohVector.z /= neighborCount;
            desiredVel = new Vector3(cohVector.x - agent.transform.position.x, 1, cohVector.z - agent.transform.position.z).normalized * Speed * Time.deltaTime;
            seekForce = (desiredVel - (agent.transform.position - prevPos));
            seekForce = new Vector3(seekForce.x * SteeringLimit, seekForce.y, seekForce.z * SteeringLimit);
            cohVector = seekForce;
        }

        // Separation Behavior
        neighborCount = 0;
        Vector3 sepVector = new Vector3(0, 0, 0);

        // Sum up distances between agent and other boids, and repel from center of mass
        for (int i = 0; i < 6; i++)
        {
            boid = agentArray[i];
            if (boid != agent)
            {
                if ((transform.position - boid.transform.position).magnitude < 3)
                {
                    sepVector += (boid.transform.position - agent.transform.position).normalized * (1 / Mathf.Pow(2, 2)) * -1;
                }
            }

        }

        // Update boids' previous positions
        for (int i = 0; i < 6; i++)
        {
            prevPosArray[i] = agentArray[i].transform.position;
        }

        // Update agent's previous position
        prevPos = transform.position;

        // Do the flocking
        Vector3 velocity = transform.position - prevPos;
        velocity.x = (velocity.x + alignVector.x * 20f + cohVector.x *10f + sepVector.x * 0.01f);
        velocity.z = (velocity.z + alignVector.z * 20f + cohVector.z *10f + sepVector.z * 0.01f);
        transform.position = transform.position + new Vector3(velocity.x, 0, velocity.z);
        

    }
}
