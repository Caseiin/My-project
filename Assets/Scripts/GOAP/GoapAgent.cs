using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoapAgent
{
    public Transform transform {get;}
    public NavMeshAgent NavAgent {get;}

    public Dictionary<string, AgentBelief> Beliefs{get;} = new ();
    public HashSet<AgentAction> Actions { get; } = new();
    public HashSet<AgentGoal> Goals { get; } = new();

    public AgentGoal CurrentGoal { get; set; }
    public Queue<AgentAction> ActionPlan { get; set; } = new();
    public AgentAction CurrentAction { get; set; }

    public GoapAgent(Transform transform, NavMeshAgent agent)
    {
        this.transform = transform;
        NavAgent = agent;
    }
}

// NavMeshAgent