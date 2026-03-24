using UnityEngine.UI;
using UnityEngine;

public class InteractiveProjectile : AbilityProjectile
{
    public Sprite InteractIcon;
    protected override void Awake()
    {
        base.Awake();
    }
    public void ToInteract(){}
    public override void Launch(Vector3 direction){}


}
