using UnityEngine;

public class PlayerController : MonoBehaviour, IEntityController
{
    // 

    [Header("Input")]
    [SerializeField] InputReader _input;
    public InputReader Input => _input;

    [Header("Movement")]
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
        machine.Update();
    }

    void FixedUpdate()
    {
        machine.FixedUpdate();
    }

    void DeclareStateInformation()
    {
        machine = new StateMachine();
    }
}
