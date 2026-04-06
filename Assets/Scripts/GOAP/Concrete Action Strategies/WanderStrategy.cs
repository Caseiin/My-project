using UnityEngine;
using UnityEngine.AI;

public class WanderStrategy : IActionStrategy
{
    readonly NavMeshAgent agent;
    readonly float wanderRadius;
    public bool CanPerform => !Complete;
    public bool Complete => agent.hasPath && agent.remainingDistance <= 2f && !agent.pathPending;

    public WanderStrategy(NavMeshAgent agent, float radius)
    {
        this.agent = agent;
        wanderRadius = radius;
    }

    public void Start(){
       for (int i =0; i < 5; i++){
            Vector2 random2D = UnityEngine.Random.insideUnitCircle * wanderRadius;
            Vector3 randomDirection = new Vector3(random2D.x, 0f, random2D.y);

            NavMeshHit hit;
            if(NavMesh.SamplePosition(agent.transform.position + randomDirection, out hit, wanderRadius, 1)){
                agent.SetDestination(hit.position);
                return;
            }
       } 
    }

    public void Stop()
    {
        agent.ResetPath();
    }

    
}
