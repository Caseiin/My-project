
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
        NavMeshAgent _navAgent;
        public NavMeshAgent NavAgent => _navAgent;

        [Header("GOAP")]
        [SerializeField] BaseActionSetup actions;
        [SerializeField] BaseBeliefSetUps beliefs;
        [SerializeField] BaseGoalsSetup goals;

        EnemyHealth _enemyHealth;
        StateMachine machine;
        GoapAgent goapAgent;
        public Transform PlayerPosition {get;private set;} = null;

        void Awake()
        {
            RB = GetComponent<Rigidbody>();
            RB.isKinematic = true;
            RB.freezeRotation = true;
            _enemyHealth = GetComponent<EnemyHealth>();
            _navAgent = GetComponent<NavMeshAgent>();

        }

        void Start(){
            goapAgent = new GoapAgent(transform, _navAgent); // Build GOAP agent
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
            beliefs.InitialiseBelief(goapAgent, this);
        }

        void SetupGoals(GoapAgent goapAgent)
        {
            goals.InitialiseGoals(goapAgent);
        }

        void SetupActions(GoapAgent goapAgent)
        {
            actions.InitializeActions(goapAgent,this);
        }

        // Helper Methods
        void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
        void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
    }
