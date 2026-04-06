using System;
using UnityEngine;
using UnityEngine.AI;

public class ChaseStrategy :IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly Transform target;
    readonly Func<bool> isInAttackRange; // delegate to the actual belief condition

    public bool CanPerform => !Complete;
    public bool Complete => isInAttackRange(); // done when belief says so, not NavMesh

    public ChaseStrategy(NavMeshAgent agent, Transform target, Func<bool> isInAttackRange)
    {
        this.agent = agent;
        this.target = target;
        this.isInAttackRange = isInAttackRange;
    }

    public void Start() { }

    public void Update(float deltaTime)
    {
        if (target != null)
            agent.SetDestination(target.position);
    }

    public void Stop()
    {
        agent.ResetPath();
    }
}
