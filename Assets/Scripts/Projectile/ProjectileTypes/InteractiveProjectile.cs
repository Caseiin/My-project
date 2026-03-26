using UnityEngine.UI;
using UnityEngine;

public class InteractiveProjectile : AbilityProjectile
{
    public Sprite InteractIcon;
    public InteractiveProjectileUI interactivePrefab;
    protected override void Awake()
    {
        base.Awake();
    }
    public void ToInteract()
    {
        Activate();
    }
    public override void Launch(Vector3 direction){}


}
