
    using System;
using System.Collections.Generic;
using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.AI;

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : EntityController, IMoveable
    {
        
        [Header("Sensor")]
        [SerializeField] Sensor detectionSensor;
        [SerializeField] Sensor attackSensor;

        public Sensor DetectionSensor => detectionSensor;
        public Sensor AttackSensor => attackSensor;

        // Movement
        [Header("Movement")]
        [SerializeField] float moveSpeed = 4f; 
        public float MoveSpeed => moveSpeed;
        public bool IsMovementBlocked { get;set;} = false;
        public Rigidbody RB {get;private set;}

        [Header("Combat")]
        [SerializeField] AttackStrategy attackStrategy;
        public AttackStrategy AttackStrategy => attackStrategy;


        EnemyHealth _enemyHealth;
        StateMachine machine;
        GoapAgent goapAgent;
        NavMeshAgent NavAgent;
        public Transform PlayerPosition {get;private set;} = null;

        void Awake()
        {
            RB = GetComponent<Rigidbody>();
            RB.isKinematic = true;
            RB.freezeRotation = true;
            _enemyHealth = GetComponent<EnemyHealth>();
            NavAgent = GetComponent<NavMeshAgent>();

        }

        void Start(){
            goapAgent = new GoapAgent(transform, NavAgent); // Build GOAP agent
            PlayerPosition =  Registry<PlayerController>.GetFirst().transform;
            DeclareStateAndGOAPInfo();
        }

        void Update()
        {
            machine?.Update();
            goapAgent?.Update(Time.deltaTime);

        }

        void FixedUpdate()
        {
            machine?.FixedUpdate();
        }

        void DeclareStateAndGOAPInfo()
        {
            machine = new StateMachine();

            SetupBeliefs(goapAgent);
            SetupActions(goapAgent);
            SetupGoals(goapAgent);


            // FSM states
            var trackState = new EnemyTrackState(this, goapAgent);
            var idlestate = new EnemyIdleState(this, goapAgent);
            var attackState = new EnemyAttackState(this, goapAgent); 
            var deathState = new EnemyDeathState(this);

            At(idlestate,   trackState,  new FuncPredicate(() => goapAgent.Beliefs["PlayerDetected"].Evaluate()));
            At(trackState,  attackState, new FuncPredicate(() => goapAgent.Beliefs["PlayerInAttackRange"].Evaluate()));
            At(attackState, trackState,  new FuncPredicate(() => !goapAgent.Beliefs["PlayerInAttackRange"].Evaluate()&& goapAgent.Beliefs["PlayerDetected"].Evaluate()));
            At(trackState,  idlestate,   new FuncPredicate(() => !goapAgent.Beliefs["PlayerDetected"].Evaluate()));

            machine.SetState(idlestate);
        }

        void SetupBeliefs(GoapAgent goapAgent)
        {
            BeliefFactory factory = new BeliefFactory(goapAgent, goapAgent.Beliefs);

            factory.AddBelief("Nothing",() => false);
            factory.AddBelief("AgentIdle",() => !NavAgent.hasPath);
            factory.AddBelief("AgentMoving",() => NavAgent.hasPath);
            factory.AddBelief("PlayerDetected", () => detectionSensor.IsTargetInRange);
            factory.AddBelief("PlayerInAttackRange", () => attackSensor.IsTargetInRange);
            factory.AddBelief("PlayerDead", () => PlayerPosition == null || PlayerPosition.GetComponent<PlayerHealth>().Health <= 0f);
            factory.AddBelief("AgentStuck", () => IsMovementBlocked);
        }

        void SetupGoals(GoapAgent goapAgent)
        {
            goapAgent.Goals.Add(new AgentGoal.Builder("ChillOut")
                .WithPriority(1)
                .WithDesiredEffect(goapAgent.Beliefs["Nothing"])
                .Build());

            goapAgent.Goals.Add(new AgentGoal.Builder("Wander")
                .WithPriority(1)
                .WithDesiredEffect(goapAgent.Beliefs["AgentMoving"])
                .Build());

            goapAgent.Goals.Add(new AgentGoal.Builder("KillPlayer")
                .WithPriority(3)
                .WithDesiredEffect(goapAgent.Beliefs["PlayerDead"])
                .Build());
        }

        void SetupActions(GoapAgent goapAgent)
        {
            goapAgent.Actions.Add(new AgentAction.Builder("Relax")
                .WithStrategy(new IdleStrategy(5))
                .AddEffect(goapAgent.Beliefs["Nothing"])
                .Build());

            goapAgent.Actions.Add(new AgentAction.Builder("Wander")
                .WithStrategy(new WanderStrategy(NavAgent, 10)) 
                .AddEffect(goapAgent.Beliefs["AgentMoving"])
                .Build());

            goapAgent.Actions.Add(new AgentAction.Builder("ChasePlayer")
                .WithStrategy(new ChaseStrategy(NavAgent,PlayerPosition,() => attackSensor.IsTargetInRange)) // truth comes from sensor, same as belief
                .AddPreCondition(goapAgent.Beliefs["PlayerDetected"])
                .AddEffect(goapAgent.Beliefs["PlayerInAttackRange"])
                .Build());

            goapAgent.Actions.Add(new AgentAction.Builder("AttackPlayer")
                .WithStrategy(new AttackActionStrategy(attackStrategy,this))
                .AddPreCondition(goapAgent.Beliefs["PlayerInAttackRange"])
                .AddEffect(goapAgent.Beliefs["PlayerDead"])
                .Build());
        }


        // float DistanceToPlayer()
        // {
        //     if (PlayerPosition == null) return Mathf.Infinity;
        //     return Vector3.Distance(transform.position, PlayerPosition.position);
        // }

        // Helper Methods
        void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
        void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
    }
