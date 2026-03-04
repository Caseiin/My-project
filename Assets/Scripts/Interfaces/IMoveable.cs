using UnityEngine;

public interface IMoveable: IEffectable
{
    Rigidbody RB {get;}
    bool IsMovementBlocked{get; set;}
}
