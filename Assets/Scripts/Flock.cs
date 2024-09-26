using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    // Defining prefab for the agents
    public FlockingAgent agentPrefab;
    List<FlockingAgent> agents = new List<FlockingAgent>();
    public FlockBehavior behavior;

    // Defining the number of agents
    [Range(10, 600)]
    public int startingSize = 300;
    const float flockDensity = 0.08f;

    // Speed of the agents
    [Range(1f, 120f)]
    public float speedFactor = 10f;
    [Range(1f, 120f)]
    public float maxSpeed = 5f;
    
    // Radius of the agents
    [Range(1f, 10f)]
    public float birdRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidMultiplier = 0.5f;

    //  Squares to avoid step of math
    float squareMaxSpeed;
    float squareBirdRadius;
    float squareAvoidRadius;
    public float SquareAvoidRadius { get { return squareAvoidRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareBirdRadius = birdRadius * birdRadius;
        squareAvoidRadius = squareBirdRadius * avoidMultiplier * avoidMultiplier;

        // Initialize the birds
        for (int i = 0; i < startingSize; i++)
        {
            FlockingAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingSize * flockDensity,
                
                // ------- Rotation -------
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );
            newAgent.name = "Seagull " + i;
            agents.Add(newAgent);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockingAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
        }
    }


    // Get the nearby objects
    List<Transform> GetNearbyObjects(FlockingAgent agent)
    {
        List<Transform> context = new List<Transform>();
        // -- Creates circle to choose what colliders are in it
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, birdRadius);
    }

}
