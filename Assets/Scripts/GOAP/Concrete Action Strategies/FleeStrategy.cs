using UnityEngine;
using UnityEngine.AI;

public class FleeStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly EnemyController enemy;
    readonly float fleeRange;
    public bool CanPerform => true;
    public bool Complete => !enemy.DetectionSensor.IsTargetInRange;

    public FleeStrategy(NavMeshAgent agent, EnemyController enemy, float range){
        this.agent = agent;
        this.enemy = enemy;
        fleeRange = range;
    }

    public void Start() => UpdateDestination();

    public void Update(float deltaTime){
        if(enemy.PlayerPosition != null)
            UpdateDestination();
    }

    void UpdateDestination(){
        Vector3 directionAwayFromPlayer =  (agent.transform.position - enemy.PlayerPosition.position).normalized;
        Vector3 fleeDestination = agent.transform.position + directionAwayFromPlayer * fleeRange;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(fleeDestination, out hit, fleeRange, NavMesh.AllAreas)){
            agent.SetDestination(fleeDestination);
        }
    }
    public void Stop() => UpdateDestination();
}
