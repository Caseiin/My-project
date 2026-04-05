using System;
using System.Collections.Generic;
using UnityEngine;

public class AgentAction
{
    public string Name{get;private set;}
    public float Cost{get;private set;}
    public HashSet<AgentBelief> Preconditions{get;} = new ();
    public HashSet<AgentBelief> Effects{get;} = new();

    Func<bool> _onPerform;
    Func<bool> _onDone;
    public bool Perform() => _onPerform();
    public bool IsDone() => _onDone();

    public AgentAction(string name)
    {
        Name = name;
    }
    public AgentAction WithCost(float cost)
    {
        Cost = cost;
        return this;
    }

    public AgentAction WithPerformance(Func<bool> func)
    {
        _onPerform = func;
        return this;
    }
    public AgentAction WithCompletion(Func<bool> func)
    {
        _onDone = func;
        return this;
    }

    public AgentAction Create()
    {
        return this;
    }
}
