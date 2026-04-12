using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [Header("Throw Info")]
    [SerializeField] List<AbilityProjectile> projectiles;
    [SerializeField] InputReader _input;
    [SerializeField] float throwForce = 10f;
    [SerializeField] float upwardForce = 2f;
    public float ThrowForce => throwForce;
    Transform throwPoint;

    int currentIndex = 0;

    void Awake()
    {
        throwPoint = transform;
    }

    public Vector3 CalculateThrowVelocity()
    {
        Vector3 dir = Camera.main.transform.forward;
        return dir * throwForce + Vector3.up * upwardForce;
    }

    void OnEnable()
    {
        
    }

    void OnDisable()
    {
        
    }

    public void Throw()
    {
        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile(projectiles[currentIndex]);

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        
        proj.transform.SetPositionAndRotation(throwPoint.position, Quaternion.identity);

        Vector3 dir = Camera.main.transform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * upwardForce;
        proj.Launch(impulse); 

        Debug.DrawRay(throwPoint.position, dir * 5f, Color.green, 2f);

    }

    void ChangeIndex(int newIndex)
    {
        currentIndex = newIndex;
    }
}
