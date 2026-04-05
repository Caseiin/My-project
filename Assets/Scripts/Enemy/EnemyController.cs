
    using System;
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
        public Transform PlayerPosition {get;private set;} = null;

        void Awake()
        {
            RB = GetComponent<Rigidbody>();
            RB.isKinematic = true;
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        void Start(){
            PlayerPosition =  Registry<PlayerController>.GetFirst().transform;
            DeclareStateInformation();
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
            var trackState = new EnemyTrackState(this, goapAgent);
            var idlestate = new EnemyIdleState(this, goapAgent);
            var attackState = new EnemyAttackState(this, goapAgent); 
            var deathState = new EnemyDeathState(this);

            At(idlestate,   trackState,  new FuncPredicate(() => detectionSensor.IsTargetInRange));
            At(trackState,  attackState, new FuncPredicate(() => attackSensor.IsTargetInRange));
            At(attackState, trackState,  new FuncPredicate(() => !attackSensor.IsTargetInRange && detectionSensor.IsTargetInRange));
            At(trackState,  idlestate,   new FuncPredicate(() => !detectionSensor.IsTargetInRange));
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
            // no op rn
        }

        private void SetupBeliefs(GoapAgent goapAgent)
        {
            var factory = new BeliefFactory(goapAgent, goapAgent.Beliefs);
            factory.AddBelief("LowHealth",() => _enemyHealth.Health < _enemyHealth.MaxHealth * 0.3f);

            factory.AddSensorBelief("PlayerInSight", detectionSensor);
            factory.AddSensorBelief("PlayerInAttackRange", attackSensor);  

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
