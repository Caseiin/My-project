using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IMoveable
{
    [Header("Movement")]
    public float MoveSpeed{get; set;} = 6f;
    public Rigidbody RB {get;private set;}
    public bool IsMovementBlocked { get;set;} = false;

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
    }

    // Helper Methods
    void At(IState from, IState to, IPredicate condition) => machine.AddTransitions(from,to,condition);
    void Any(IState to, IPredicate condition) => machine.AddAnyTransition(to,condition);
}
