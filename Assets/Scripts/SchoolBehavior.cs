using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SchoolBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove (SchoolingAgent agent, List<Transform> context, FishSchool school);

}
