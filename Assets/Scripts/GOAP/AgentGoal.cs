using System.Collections.Generic;
using UnityEngine;

public class AgentGoal
{
    public string Name {get;}
    public int Priority{get;}
    public Dictionary<AgentBelief, bool> DesiredEffects {get;} = new();

    public AgentGoal(string name, int priority)
    {
        Name = name;
        Priority = priority;
    }
}
