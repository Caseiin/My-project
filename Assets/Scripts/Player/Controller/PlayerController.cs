using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : EntityController,IMoveable,IPlayerEffectable
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
    public Rigidbody RB {get;private set;}
    public bool IsMovementBlocked { get;set;} = false;

    [Header("Sensitivity")]
    public Transform _headTransform;
    public float _sensitivity = .1f;
    public float _verticalClamp = 80f;
    public float _lookSmoothTime = 0.05f; // small = snappy, large = more smooth

    [Header("Throw")]
    public GameObject Hand;
    public  ProjectileThrow ThrowLogic{get; set;}
    public  TrajectorPredictor Trajectory{get; set;}

    
    // Camera motion logic
    [Header("Camera Behaviour")]
    [Range(0,100)]
    public float ViewRange = 30f;
    CameraLogic _cameraLogic;

    // StateMachine & state declaration
    StateMachine machine;

    void Awake()
    {
        Input.EnableInputMap();
        RB = GetComponent<Rigidbody>();
        ThrowLogic = Hand.GetComponent<ProjectileThrow>();
        Trajectory = Hand.GetComponentInChildren<TrajectorPredictor>();


        DeclareStateInformation();
        SetCameraLogic(new FPSCameraLogic(this)); 
    }

    void Update()
    {
        _cameraLogic.HandleLook();
        // Aim(Input.IsAimming);
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
        var throwstate = new PlayerThrowState(this);

        // Any(throwstate, new FuncPredicate(() => _input.IsAimming));

        // Define transitions
        At(idlestate,motionstate,new FuncPredicate(()=> _input.MoveDirection.sqrMagnitude > _movementThreshold));
        At(motionstate,idlestate,new FuncPredicate(()=> _input.MoveDirection.sqrMagnitude <= _movementThreshold));
        // At(throwstate, idlestate, new FuncPredicate(() => !_input.IsAimming && _input.MoveDirection.sqrMagnitude <= _movementThreshold));
        // At(throwstate, motionstate, new FuncPredicate(() => !_input.IsAimming && _input.MoveDirection.sqrMagnitude > _movementThreshold));

        machine.SetState(idlestate);
    }

    public void SetCameraLogic(CameraLogic logic)
    {
        _cameraLogic = logic;
    }

    public void HandleThrow()
    {
        ThrowLogic.Throw();
    }

    public void Aim(bool IsAimming)
    {
        if (IsAimming)
        {
            var velocity = ThrowLogic.CalculateThrowVelocity();
            Trajectory.Predict(Hand.transform.position,velocity);
        }
        else
        {
            Trajectory.StopPrediction();
        }
    }

    void OnEnable()
    {
        // Input.OnAttackTriggered += HandleThrow;
    }

    void OnDisable()
    {
        // Input.OnAttackTriggered -= HandleThrow; 
    }

    // Helper Methods
    void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
    void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
}
