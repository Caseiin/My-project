using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : EntityController, IMoveable
{
    public Transform PlayerPosition; 
    [SerializeField]float _detectionRange = 15f; 
    [SerializeField]float _attackRange = 2.5f; 


    [Header("Movement")]
    public float MoveSpeed{get; set;} = 4f;
    public Rigidbody RB {get;private set;}
    public bool IsMovementBlocked { get;set;} = false;

    bool isIdling = false;

    StateMachine machine;

    void Awake()
    {
        RB = GetComponent<Rigidbody>();
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

        var trackState = new EnemyTrackState(this);
        var idlestate = new EnemyIdleState(this);
        var attackState = new EnemyAttackState(this);

        At(idlestate, trackState, new FuncPredicate(() => DistanceToPlayer() < _detectionRange));

        At(trackState, attackState, new FuncPredicate(() => DistanceToPlayer() <= _attackRange));

        At(attackState, trackState, new FuncPredicate(() =>  DistanceToPlayer() > _attackRange && DistanceToPlayer()< _detectionRange));

        At(trackState, idlestate, new FuncPredicate(() =>  DistanceToPlayer() >=  _detectionRange));

        machine.SetState(trackState);
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
