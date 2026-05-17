using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class InteractiveProjectile : AbilityProjectile
{   
    public InteractiveProjectileUI interactivePrefab;
    protected override void Awake()
    {
        base.Awake();
    }
    public void ToInteract()
    {
        ShowImpactRange();
    }

    public override void ShowImpactRange()
    {
        Range.SetActive(true);
        var scale = 2 * MaxEffectRadius;
        Range.transform.DOScale(new Vector3(scale,scale,scale),3f)
                .OnComplete(()=>Activate());
    }
}
