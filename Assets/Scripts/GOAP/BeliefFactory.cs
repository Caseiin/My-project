using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BeliefFactory
{
    readonly GoapAgent agent;
    readonly Dictionary<string, AgentBelief> beliefs;

    public BeliefFactory(GoapAgent agent, Dictionary<string,AgentBelief> beliefs)
    {
        this.agent = agent;
        this.beliefs = beliefs;
    }

    public void AddBelief(string key, Func<bool> condition){
        beliefs.Add(key,new AgentBelief.Builder(key)
                        .WithCondition(condition)
                        .Build());
    }


    public void AddLocationBelief(string key,float distance , Transform locationcondition){
        AddLocationBelief(key,distance,locationcondition.position);
    } 

    public void AddLocationBelief(string key,float distance , Vector3 locationcondition){
        beliefs.Add(key,new AgentBelief.Builder(key)
                        .WithCondition(()=> InRangeOf(locationcondition, distance))
                        .WithLocation(()=> locationcondition)
                        .Build());
    }

    public void AddSensorBelief(string key,Sensor sensor){
        beliefs.Add(key,new AgentBelief.Builder(key)
                        .WithCondition(()=> sensor.IsTargetInRange)
                        .WithLocation(()=> sensor.TargetPosition)
                        .Build());
    }

    bool InRangeOf(Vector3 pos, float range)=> Vector3.Distance(agent.transform.position, pos) <range;
}
