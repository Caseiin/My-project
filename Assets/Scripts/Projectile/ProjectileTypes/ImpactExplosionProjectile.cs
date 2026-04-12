using UnityEngine;
using DG.Tweening;

public class ImpactExplosionProjectile : AbilityProjectile
{
    public override void ShowImpactRange()
    {
        Range.SetActive(true);
        var scale = 2 * MaxEffectRadius;
        Range.transform.DOScale(new Vector3(scale,scale,scale),0.5f)
                        .OnComplete(() => Activate());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            ResetPhysics();
            ShowImpactRange();
        }
    }
}
