using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(SchoolingAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        // ---- For each transform, is it a flock agent? if true then add to the list ---- 
        foreach (Transform item in original)
        {
            SchoolingAgent itemAgent = item.GetComponent<SchoolingAgent>();
            if (itemAgent != null && itemAgent.AgentSchool == agent.AgentSchool)
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
