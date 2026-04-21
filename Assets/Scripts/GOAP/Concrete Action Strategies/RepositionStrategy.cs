
using UnityEngine;
using UnityEngine.AI;

public class RepositionStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly Sensor attackSensor;
    readonly float repositionRadius;
    readonly float newAttackRadius;


    public bool CanPerform  => !Complete;
    public bool Complete => agent.hasPath && agent.remainingDistance <= 2f && !agent.pathPending;

    public RepositionStrategy(NavMeshAgent agent,float radius, Sensor sensor, float sensorRadius){
        this.agent = agent;
        repositionRadius = radius;
        attackSensor = sensor;
        newAttackRadius = sensorRadius;
    }

    public void Start(){
        var random2D = Random.insideUnitCircle* repositionRadius;
        var randomDirection = new Vector3(random2D.x,0f, random2D.y);

        NavMeshHit hit;
        if(NavMesh.SamplePosition(agent.transform.position + randomDirection, out hit, repositionRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }

    public void Stop(){
        agent.ResetPath();
        attackSensor.Radius = newAttackRadius;
    }




}
