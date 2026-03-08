using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : EntityController, IMoveable
{
    public Transform PlayerPosition; 
    [SerializeField]float detectionRange = 15f; 

    [Header("Movement")]
    public float MoveSpeed{get; set;} = 6f;
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
        var idlestate =  new EnemyIdleState(this);

    At(idlestate, trackState, new FuncPredicate(() => 
        PlayerPosition != null && Vector3.Distance(transform.position, PlayerPosition.position) < detectionRange));

    At(trackState, idlestate, new FuncPredicate(() => 
        PlayerPosition == null || Vector3.Distance(transform.position, PlayerPosition.position) >= detectionRange));

        machine.SetState(trackState);
    }

    // Helper Methods
    void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
    void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
}
