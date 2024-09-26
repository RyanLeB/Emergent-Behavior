using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Collider2D))]
public class FlockingAgent : MonoBehaviour
{

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }


    // The collider of the agent
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }



    public void Move(Vector2 velocity)
    {
        // Makes the asset face the direction it is moving
        transform.up = velocity;
        
        
        // Casting the velocity to Vector3 to move the agent
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
