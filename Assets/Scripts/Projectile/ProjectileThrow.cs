using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [Header("Throw Info")]
    [SerializeField] List<AbilityProjectile> projectiles;
    [SerializeField] InputReader _input;
    [SerializeField] float _cooldownDuration = 5f;
    [SerializeField] int _maxCount = 5;
    [SerializeField] float throwForce = 10f;
    [SerializeField] float upwardForce = 2f;
    public float ThrowForce => throwForce;
    Transform throwPoint;
    AbilitySO[] _selectedAbility;
    int currentIndex = 0;
    bool CanThrow = true;
    int _currentCount = 0;

    CountdownTimer _countdown;

    void Awake()
    {
        throwPoint = transform;
        _countdown = new CountdownTimer(_cooldownDuration);
        // initialise with projectiles's default Ability
        _selectedAbility = new AbilitySO[projectiles.Count];
        for(int i = 0; i < projectiles.Count; i++){
            _selectedAbility[i] = projectiles[i].ability;
        }
    }

    void Update()
    {
        _countdown.Tick(Time.deltaTime);
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
        // TODO: Add logic to limit throwing whether that be a cooldown or a throw amount limit
        if(!CanThrow || _currentCount >= _maxCount) return;
        
        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile(projectiles[currentIndex]);
        _currentCount ++;

        if (_selectedAbility != null)
            proj.SetAbility(_selectedAbility[currentIndex]);

        
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        proj.transform.SetPositionAndRotation(throwPoint.position, Quaternion.identity);

        Vector3 dir = Camera.main.transform.forward;
        Vector3 impulse = dir * throwForce + Vector3.up * upwardForce;
        proj.Launch(impulse);

        if(_currentCount >= _maxCount){
            StartThrowCoolDown();
        }
    }

    void SetProjectileAbility(AbilitySO ability) => _selectedAbility[currentIndex] = ability;
    void ChangeIndex(int newIndex) {
        currentIndex = newIndex;
        string msg = (newIndex > 0)? "Impact Bomb": "Interactive Bomb";
        Debug.Log($"Equipped: {msg}");
        Messenger.AddEquipMessage(msg);
    }

    void ResetThrow(){
        CanThrow = true;
        _currentCount = 0;
        Debug.Log("Throw CoolDown ended!");
    }

    void StartThrowCoolDown(){
        _countdown.OnTimerStart = ()=> CanThrow = false;
        _countdown.OnTimerStop = ()=> ResetThrow();
        _countdown.Start();
        Debug.Log("Throw CoolDown started!");
    }
}
