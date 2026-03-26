using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce;
    public float ThrowForce => throwForce;

    public void Throw()
    {
        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile();

        proj.transform.position = throwPoint.position;
        proj.transform.rotation = throwPoint.rotation;

        Vector3 direction = throwPoint.forward;
        proj.Launch(direction * throwForce);
    }
}
