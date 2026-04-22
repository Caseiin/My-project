
using UnityEngine;
using UnityEngine.AI;

public class RepositionStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly Sensor attackSensor;
    readonly float repositionRadius;
    readonly float newAttackRadius;
    bool _started = false;

    public bool CanPerform  => !Complete;
    public bool Complete => _started && !agent.pathPending && agent.hasPath && agent.remainingDistance <= 1f;

    public RepositionStrategy(NavMeshAgent agent,float radius, Sensor sensor, float sensorRadius){
        this.agent = agent;
        repositionRadius = radius;
        attackSensor = sensor;
        newAttackRadius = sensorRadius;
    }

    public void Start(){
        _started  = false;
        var random2D = Random.insideUnitCircle* repositionRadius;
        var randomDirection = new Vector3(random2D.x,0f, random2D.y);

        NavMeshHit hit;
        if(NavMesh.SamplePosition(agent.transform.position + randomDirection, out hit, repositionRadius, 1))
        {
            agent.SetDestination(hit.position);
            _started = true;
        }
    }

    public void Stop(){
        agent.ResetPath();
        attackSensor.Radius = newAttackRadius;
    }




}
