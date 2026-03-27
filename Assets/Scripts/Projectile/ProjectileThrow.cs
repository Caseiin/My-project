using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce;
    public float ThrowForce => throwForce;

    void Awake()
    {
        if (throwPoint == null)
        {
            Debug.LogError("throwPoint is not assigned on ProjectileThrow!");
            throwPoint = transform;
        }
    }
    public void Throw()
    {
        Debug.Log($"Throw() called. throwPoint={throwPoint}, Manager={ProjectileManager.Instance}");
        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile();
        Debug.Log($"Got projectile: {proj}");
        proj.transform.position = throwPoint.position;
        proj.transform.rotation = throwPoint.rotation;
        proj.Launch(throwPoint.forward * throwForce);
    }
}
