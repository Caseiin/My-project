using UnityEngine.UI;
using UnityEngine;

public class InteractiveProjectile : AbilityProjectile
{
    public InteractiveProjectileUI interactivePrefab;
    
    protected override void Awake()
    {
        base.Awake();

    }
    public void ToInteract()
    {
        Activate();
    }
    
}
