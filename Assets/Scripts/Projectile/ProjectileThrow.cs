using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce = 10f;
    [SerializeField] float upwardForce = 2f;
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
        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile();

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        
        proj.transform.SetPositionAndRotation(throwPoint.position, Quaternion.identity);

        Vector3 dir = Camera.main.transform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * upwardForce;
        proj.Launch(impulse); 

        Debug.DrawRay(throwPoint.position, dir * 5f, Color.green, 2f);

    }
}
