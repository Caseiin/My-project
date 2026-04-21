using UnityEngine;

public abstract class BaseBeliefSetUps : ScriptableObject
{
    public virtual void InitialiseBelief(GoapAgent agent, EnemyController enemy){
        BeliefFactory factory = new BeliefFactory(agent, agent.Beliefs);

        factory.AddBelief("Nothing",() => false);
        factory.AddBelief("AgentIdle",() => !enemy.NavAgent.hasPath);
        factory.AddBelief("AgentMoving",() => enemy.NavAgent.hasPath);
        factory.AddBelief("PlayerDetected", () => enemy.DetectionSensor.IsTargetInRange);
        factory.AddBelief("PlayerInAttackRange", () => enemy.AttackSensor.IsTargetInRange);
        factory.AddBelief("PlayerDead", () => enemy.PlayerPosition == null || enemy.PlayerPosition.GetComponent<PlayerHealth>()?.Health <= 0f);
        factory.AddBelief("AgentStuck", () => enemy.IsMovementBlocked);
        factory.AddBelief("AgentNearDeath", ()=> enemy.Health.Health <= enemy.Health.MaxHealth * 0.3f);
    }
}
