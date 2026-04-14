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
    AbilitySO[] _selectedAbility;
    int currentIndex = 0;

    void Awake()
    {
        throwPoint = transform;

        // initialise with projectiles's default Ability
        _selectedAbility = new AbilitySO[projectiles.Count];
        for(int i = 0; i < projectiles.Count; i++){
            _selectedAbility[i] = projectiles[i].ability;
        }
    }

    public Vector3 CalculateThrowVelocity()
    {
        Vector3 dir = Camera.main.transform.forward;
        return dir * throwForce + Vector3.up * upwardForce;
    }

    void OnEnable()
    {
        _input.OnHotBarTriggered += ChangeIndex;
        RingMenu.onAbiiltySelected += SetProjectileAbility;
    }

    void OnDisable()
    {
        _input.OnHotBarTriggered -= ChangeIndex;
        RingMenu.onAbiiltySelected -= SetProjectileAbility;
    }

    public void Throw()
    {
        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile(projectiles[currentIndex]);

        if (_selectedAbility != null)
            proj.SetAbility(_selectedAbility[currentIndex]);

        
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        
        proj.transform.SetPositionAndRotation(throwPoint.position, Quaternion.identity);

        Vector3 dir = Camera.main.transform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * upwardForce;
        proj.Launch(impulse); 

        Debug.DrawRay(throwPoint.position, dir * 5f, Color.green, 2f);

    }

    void ChangeIndex(int newIndex) => currentIndex = newIndex;
    void SetProjectileAbility(AbilitySO ability) => _selectedAbility[currentIndex] = ability;
}
