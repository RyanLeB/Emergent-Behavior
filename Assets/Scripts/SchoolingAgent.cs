using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Collider2D))]
public class SchoolingAgent : MonoBehaviour
{

    FishSchool agentSchool;
    public FishSchool AgentSchool { get { return agentSchool; } }


    // The collider of the agent
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(FishSchool school)
    {
        agentSchool = school;
    }

    //public Vector2 AvoidColliders(float avoidRadius)
    //{
    //    Vector2 avoidanceMove = Vector2.zero;
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, avoidRadius);

    //    foreach (Collider2D collider in colliders)
    //    {
    //        if (collider != AgentCollider)
    //        {
    //            Vector2 directionToCollider = (Vector2)transform.position - (Vector2)collider.transform.position;
    //            avoidanceMove += directionToCollider;
    //        }
    //    }

    //    return avoidanceMove;
    //}

    //public Vector2 SeparateFromOtherAgents(float separationRadius)
    //{
    //    Vector2 separationMove = Vector2.zero;
    //    int nAvoid = 0;
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, separationRadius);

    //    foreach (Collider2D collider in colliders)
    //    {
    //        FlockingAgent otherAgent = collider.GetComponent<FlockingAgent>();
    //        if (otherAgent != null && otherAgent != this)
    //        {
    //            Vector2 directionToOtherAgent = (Vector2)transform.position - (Vector2)otherAgent.transform.position;
    //            separationMove += directionToOtherAgent;
    //            nAvoid++;
    //        }
    //    }

    //    if (nAvoid > 0)
    //    {
    //        separationMove /= nAvoid;
    //    }

    //    return separationMove;
    //}


    public void Move(Vector2 velocity)
    {
        // Makes the asset face the direction it is moving
        transform.up = velocity;
        
        
        // Casting the velocity to Vector3 to move the agent
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
