using System;
using UnityEngine;

public class PlayerController : EntityController
{
    //Provides Info about player to other classes

    [Header("Input")]
    [SerializeField] InputReader _input;
    public InputReader Input => _input;

    [Header("Movement")]
    [SerializeField] float _movementThreshold = 0.05f;
    public float MoveSpeed{get; set;} = 6f;
    public float JumpForce{get; set;} = 8f;
    public float FallMultiplier{get; set;} = 2.5f;
    public Rigidbody RB {get; private set;}

    // StateMachine & state declaration
    StateMachine machine;

    void Awake()
    {
        Input.EnableInputMap();
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

        // Declare states
        var motionstate = new PlayerMotionState(this);
        var idlestate = new PlayerIdleState(this);

        // Define transitions
        At(idlestate,motionstate,new FuncPredicate(()=> _input.MoveDirection.sqrMagnitude > _movementThreshold));
        At(motionstate,idlestate,new FuncPredicate(()=> _input.MoveDirection.sqrMagnitude <= _movementThreshold));

        machine.SetState(idlestate);

    }

    // Helper Methods
    void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
    void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
}
