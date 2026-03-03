using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly PlayerController player;
    protected BaseState(PlayerController player)
    {
        this.player = player;
    }
    public virtual void FixedUpdate()
    {
        // no op
    }

    public virtual void OnEnter()
    {
        // no op
    }

    public virtual void OnExit()
    {
        // no op
    }

    public virtual void Update()
    {
        // no op
    }
}
