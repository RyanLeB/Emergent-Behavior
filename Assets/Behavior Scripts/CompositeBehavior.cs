using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : SchoolBehavior
{

    public SchoolBehavior[] behaviors;
    public float[] weights;

    public override Vector2 CalculateMove(SchoolingAgent agent, List<Transform> context, FishSchool school)
    {

        // ---- Handles data mismatch ----
        if (weights.Length != behaviors.Length)
        {
            Debug.LogError("Mismatch in " + name, this);
            return Vector2.zero;
        }

        // ---- Set up move ----
        Vector2 move = Vector2.zero;

        // ---- Iterate through behaviors ----
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMove(agent, context, school) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }


        // ---- To avoid other agents ----

        //// Add avoidance move
        //float avoidRadius = 1.0f; // Adjust this value as needed
        //Vector2 avoidanceMove = agent.AvoidColliders(avoidRadius);
        //move += avoidanceMove;

        //// Add separation move
        //float separationRadius = 1.0f; // Adjust this value as needed
        //Vector2 separationMove = agent.SeparateFromOtherAgents(separationRadius);
        //move += separationMove;
        return move;
    }
}
