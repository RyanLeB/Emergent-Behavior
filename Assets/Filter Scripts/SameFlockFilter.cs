using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockingAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        // ---- For each transform, is it a flock agent? if true then add to the list ---- 
        foreach (Transform item in original)
        {
            FlockingAgent itemAgent = item.GetComponent<FlockingAgent>();
            if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
