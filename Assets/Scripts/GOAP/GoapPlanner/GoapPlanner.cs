using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoapPlanner : IGoapPlanner
{
    public ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null)
    {
        // Order goals by priority, descending
        List<AgentGoal> orderedGoals = goals
            .Where(g => g.DesiredEffects.Any(b => !b.Evaluate()))
            .OrderByDescending(g => g == mostRecentGoal? g.Priority - 1 : g.Priority)
            .ToList();

        // Try to solve each goal in order
        foreach(var goal in orderedGoals)
        {
            // Using depth first search
            // Start from final goal

            Node goalNode = new Node(null, null, goal.DesiredEffects, 0);
            
            // If we can find a path to the goal,return the plan
            if(FindPath(goalNode, agent.Actions))
            {
                // if the goalNode has no leaves and no action to perform try a different goal
                if (goalNode.IsLeafDead)
                {
                    Debug.Log($"Goal {goal.Name} has dead leaf, skipping");
                    continue;
                }
                Debug.Log($"Found plan for goal: {goal.Name}");

                Stack<AgentAction> actionStack = new();
                while (goalNode.Leaves.Count > 0){
                    var cheapestLeaf = goalNode.Leaves.OrderBy(leaf => leaf.Cost).First();
                    goalNode = cheapestLeaf;
                    actionStack.Push(cheapestLeaf.Action);
                }

                return new ActionPlan(goal, actionStack, goalNode.Cost);
            }
            else
            {
                Debug.Log($"No path found for goal: {goal.Name}");
            }
        }

        Debug.LogWarning("No plan found");
        return null;
    }

    bool FindPath(Node parent, HashSet<AgentAction> actions){
        foreach (var action in actions){
            var requiredEffects = parent.RequiredEffects;

            // remove any effects that evaluate to true, there is no actions to take
            requiredEffects.RemoveWhere(b=> b.Evaluate());

            // if there are no required effects tot fulfill, we have a plan
            if (requiredEffects.Count == 0){
                return true;
            }

            if (action.Effects.Any(requiredEffects.Contains))
            {
                var newRequiredEffects = new HashSet<AgentBelief>(requiredEffects);
                newRequiredEffects.ExceptWith(action.Effects);
                newRequiredEffects.UnionWith(action.Preconditions);

                var newAvailableActions = new HashSet<AgentAction>(actions);
                newAvailableActions.Remove(action);

                var newNode = new Node(parent, action, newRequiredEffects, parent.Cost + action.Cost);

                // Explore new node recursively
                if(FindPath(newNode, newAvailableActions)){
                    parent.Leaves.Add(newNode);
                    newRequiredEffects.ExceptWith(newNode.Action.Preconditions);
                }

                // if all effects at this depth have been satisfied, return true
                if (newRequiredEffects.Count == 0) return true;
            }
        }
        return false;
    }
}
