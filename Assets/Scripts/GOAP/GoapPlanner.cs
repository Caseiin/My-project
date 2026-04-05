using System.Collections.Generic;
using System.Linq;
public static class GoapPlanner
{
    public static Queue<AgentAction> Plan(GoapAgent agent, AgentGoal goal)
    {
        var usableActions = agent.Actions
            .Where(a => a.Preconditions.All(p => p.Evaluate()))
            .ToList();

        // Simple greedy planner — sort by cost, return cheapest valid sequence
        // A full implementation uses A* over belief states
        var plan = new List<AgentAction>();
        var currentEffects = new HashSet<string>(
            agent.Beliefs.Where(b => b.Value.Evaluate()).Select(b => b.Key)
        );

        foreach (var desired in goal.DesiredEffects)
        {
            if (!desired.Value || currentEffects.Contains(desired.Key.Name)) continue;

            var action = usableActions
                .Where(a => a.Effects.Any(e => e.Name == desired.Key.Name))
                .OrderBy(a => a.Cost)
                .FirstOrDefault();

            if (action != null) plan.Add(action);
        }

        return new Queue<AgentAction>(plan);
    }
}
