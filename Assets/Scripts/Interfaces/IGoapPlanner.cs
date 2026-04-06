using System.Collections.Generic;
using UnityEngine;

public interface IGoapPlanner
{
    ActionPlan Plan(GoapAgent agent, HashSet<AgentGoal> goals, AgentGoal mostRecentGoal = null);
}
