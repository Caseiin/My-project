using System;
using UnityEngine;

public class AgentBelief
{
    public string Name {get;}
    Func<bool> Condition = ()=> false;
    Func<Vector3> observedLocation = ()=> Vector3.zero;
    public Vector3 Location => observedLocation();

    public AgentBelief(string name)
    {
        Name = name;
    }

    public bool Evaluate()=> Condition();

    public class Builder
{
    readonly AgentBelief _belief;

    public Builder(string name)
    {
        _belief = new AgentBelief(name);
    }

    public Builder WithCondition(Func<bool> condition)
    {
        _belief.Condition = condition;
        return this;
    }

    public Builder WithLocation(Func<Vector3> location)
    {
        _belief.observedLocation = location;
        return this;
    }

    public AgentBelief Build()
    {
        return _belief;
    }
}
}
