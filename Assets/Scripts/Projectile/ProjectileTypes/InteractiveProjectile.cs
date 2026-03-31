using UnityEngine.UI;
using UnityEngine;

public class InteractiveProjectile : AbilityProjectile
{
    public InteractiveProjectileUI interactivePrefab;
    public GameObject RangeIndicator;
    
    protected override void Awake()
    {
        base.Awake();
        DisableRange();
    }
    public void ToInteract()
    {
        Activate();
    }

    public void EnableRange()
    {
        RangeIndicator.SetActive(true);
    }

    public void DisableRange()
    {
        RangeIndicator.SetActive(false);
    }
    
}
