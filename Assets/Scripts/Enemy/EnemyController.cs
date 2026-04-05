
using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : EntityController, IMoveable
{
    public Transform PlayerPosition {get;private set;} = null;
    [SerializeField]float _detectionRange = 15f; 
    [SerializeField]float _attackRange = 2.5f; 


    [Header("Movement")]
    public float MoveSpeed{get; set;} = 4f;
    public Rigidbody RB {get;private set;}
    public bool IsMovementBlocked { get;set;} = false;

    EnemyHealth _enemyHealth;
    StateMachine machine;
    GoapAgent goapAgent;

    void Awake()
    {
        RB = GetComponent<Rigidbody>();
        _enemyHealth = GetComponent<EnemyHealth>();
        DeclareStateInformation();
    }

    void Start(){
        PlayerPosition =  Registry<PlayerController>.GetFirst().transform;
    }

    void Update()
    {
        machine?.Update();
    }

    void FixedUpdate()
    {
        machine?.FixedUpdate();
    }

    void DeclareStateInformation()
    {
        machine = new StateMachine();

        // Build GOAP agent
        var NavAgent = GetComponent<NavMeshAgent>();
        goapAgent = new GoapAgent(transform, NavAgent);
        SetupBeliefs(goapAgent);
        SetupActions(goapAgent);
        SetupGoals(goapAgent);

        // FSM states
        var trackState = new EnemyTrackState(this);
        var idlestate = new EnemyIdleState(this);
        var attackState = new EnemyAttackState(this);
        var deathState = new EnemyDeathState(this);

        At(idlestate, trackState, new FuncPredicate(() => DistanceToPlayer() < _detectionRange));
        At(trackState, attackState, new FuncPredicate(() => DistanceToPlayer() <= _attackRange));
        At(attackState, trackState, new FuncPredicate(() =>  DistanceToPlayer() > _attackRange && DistanceToPlayer()< _detectionRange));
        At(trackState, idlestate, new FuncPredicate(() =>  DistanceToPlayer() >=  _detectionRange));
        Any(deathState, new FuncPredicate(()=> _enemyHealth.Health <= 0f));

        machine.SetState(idlestate);
    }

    private void SetupGoals(GoapAgent goapAgent)
    {
        var killGoal = new AgentGoal("KillPlayer", priority: 10);
        killGoal.DesiredEffects[goapAgent.Beliefs["PlayerInSight"]] = false; // player dead = out of sight
        goapAgent.Goals.Add(killGoal);
    }

    private void SetupActions(GoapAgent goapAgent)
    {
        var nav = GetComponent<NavMeshAgent>();

        goapAgent.Actions.Add(new AgentAction("MoveToPlayer")
                                    .WithCost(1f)
                                    .WithPerformance(()=> {nav.SetDestination(PlayerPosition.position); return true;})
                                    .WithCompletion(()=> DistanceToPlayer()<= _attackRange));
    }

    private void SetupBeliefs(GoapAgent goapAgent)
    {
        var factory = new BeliefFactory(goapAgent, goapAgent.Beliefs);

        factory.AddBelief("PlayerInSight",
            () => PlayerPosition != null && DistanceToPlayer() < _detectionRange);

        // factory.AddSensorBelief("PlayerInAttackRange",
        //     new ProximitySensor(this, _attackRange));   // your existing sensor type

        // factory.AddBelief("LowHealth",
        //     () => _enemyHealth.Health < _enemyHealth.MaxHealth * 0.3f);
    }

    float DistanceToPlayer()
    {
        if (PlayerPosition == null) return Mathf.Infinity;
        return Vector3.Distance(transform.position, PlayerPosition.position);
    }

    // Helper Methods
    void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
    void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
}
