using UnityEngine;

public abstract class BaseState : IState
{
    protected readonly EntityController entity;
    protected BaseState(EntityController entity)
    {
        this.entity = entity;
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
