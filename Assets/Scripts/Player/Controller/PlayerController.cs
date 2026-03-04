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

    [Header("Sensitivity")]
    [SerializeField] Transform _headTransform;
    [SerializeField] float _sensitivity = .1f;
    [SerializeField] float _verticalClamp = 80f;
    [SerializeField] float _lookSmoothTime = 0.05f; // small = snappy, large = more smooth
    Vector2 _currentLook; // current smoothed input
    Vector2 _lookVelocity; // used internally for SmoothDamp    

    float _xRotation;

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
        HandleLook();
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

    void HandleLook()
    {
        // Smooth input first
        _currentLook.x = Mathf.SmoothDamp(_currentLook.x, _input.LookDirection.x, ref _lookVelocity.x, _lookSmoothTime);
        _currentLook.y = Mathf.SmoothDamp(_currentLook.y, _input.LookDirection.y, ref _lookVelocity.y, _lookSmoothTime);

        // Apply sensitivity
        float mouseX = _currentLook.x * _sensitivity;
        float mouseY = _currentLook.y * _sensitivity;

        // Rotate body (yaw)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate head (pitch)
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_verticalClamp, _verticalClamp);

        _headTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
    }

    // Helper Methods
    void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
    void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
}
