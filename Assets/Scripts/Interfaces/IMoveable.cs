using UnityEngine;

public interface IMoveable: IEffectable
{
    Rigidbody RB {get;}
    Transform Transform{get;}
    bool IsMovementBlocked{get; set;}
}
