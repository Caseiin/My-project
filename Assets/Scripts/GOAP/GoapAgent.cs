using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GoapAgent
{
    public Transform transform {get;}
    public NavMeshAgent NavAgent {get;}

    public Dictionary<string, AgentBelief> Beliefs{get;} = new ();
    public HashSet<AgentAction> Actions { get; } = new();
    public HashSet<AgentGoal> Goals { get; } = new();

    public AgentGoal lastGoal{get; set;}
    public AgentGoal CurrentGoal { get; set; }
    public ActionPlan ActionPlan { get; set; }
    public AgentAction CurrentAction { get; set; }

    public IGoapPlanner Planner{get;}

    public GoapAgent(Transform transform, NavMeshAgent agent)
    {
        this.transform = transform;
        NavAgent = agent;
        Planner = new GoapPlanner();
    }

    public void CalculatePlan(){
        var priorityLevel = CurrentGoal?.Priority?? 0;

        HashSet<AgentGoal> goalsToCheck = Goals;

        // if we have a current goal, we only want to check goals with higher priority
        if( CurrentGoal != null){
            Debug.Log("Current goal exists, checking goals with higher priority");
            goalsToCheck = new HashSet<AgentGoal>(Goals.Where(g => g.Priority > priorityLevel));
        }

        var potentialPlan = Planner.Plan(this, goalsToCheck, lastGoal);
        if(potentialPlan != null){
            Debug.Log($"Selected goal: {potentialPlan.AgentGoal.Name}");
            ActionPlan = potentialPlan;
        }
    }

    public void Update(float deltaTime)
    {
        if (CurrentAction == null)
        {
            CalculatePlan();

            if (ActionPlan != null && ActionPlan.Actions.Count > 0)
            {
                CurrentGoal = ActionPlan.AgentGoal;
                CurrentAction = ActionPlan.Actions.Pop();
                CurrentAction.Start();
                Debug.Log($"Starting action: {CurrentAction.Name}");
            }
        }

        if (CurrentAction != null)
        {
            bool stuck = Beliefs["AgentStuck"].Evaluate();
            NavAgent.isStopped = stuck;

            if (!stuck) CurrentAction.Update(deltaTime);

            if (CurrentAction.Complete)
            {
                Debug.Log($"{CurrentAction.Name} complete");
                CurrentAction.Stop();
                CurrentAction = null;

                if (ActionPlan.Actions.Count == 0)
                {
                    Debug.Log("Plan complete");
                    lastGoal = CurrentGoal;
                    CurrentGoal = null;
                    ActionPlan = null; // clear so CalculatePlan runs fresh
                }
            }
        }
    }
}

