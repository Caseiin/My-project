using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [SerializeField] Transform throwPoint;
    [SerializeField] float throwForce = 20f;
    [SerializeField] float upwardForce = 6f;
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
        
        // Step 1: Stop physics while positioning
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Step 2: Move projectile to hand / throw point
        proj.transform.position = throwPoint.position;
        proj.transform.rotation = Quaternion.identity;

        // Debug
        Vector3 dir = Camera.main.transform.forward;
        Debug.DrawRay(throwPoint.position, dir * 5f, Color.green, 2f);

        // Step 3: Launch immediately
        Vector3 impulse = dir * throwForce + Vector3.up * upwardForce;
        proj.Launch(impulse);  // sets isKinematic=false and adds force
    }
}
