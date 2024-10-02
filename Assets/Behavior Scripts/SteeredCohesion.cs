using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesion : FilteredSchoolBehavior
{
    Vector2 currentVelocity;
    public float smoothTime = 0.5f;

    public override Vector2 CalculateMove(SchoolingAgent agent, List<Transform> context, FishSchool school)
    {
        // ---- This basically says if there are no neighbors, return no adjustment in movement ----
        if (context.Count == 0)
            return Vector2.zero;


        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;


        // ---- Create offset (including smoothing) ----
        cohesionMove -= (Vector2)agent.transform.position;
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, smoothTime);
        return cohesionMove;


    }
}
