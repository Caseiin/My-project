using System.Collections.Generic;
using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [Header("Throw Info")]
    [SerializeField] List<AbilityProjectile> _projectiles;
    [SerializeField] InputReader _input;
    [SerializeField] float _cooldownDuration = 5f;
    [SerializeField] int _maxThrowCount = 5;
    [SerializeField] float _throwForce = 10f;
    [SerializeField] float upwardForce = 2f;
    public float ThrowForce => _throwForce;

    int currentIndex;
    int _currentThrowCount;
    AbilitySO[] _selectedAbility;
    CountdownTimer _cooldownTimer;
    Transform throwPoint;

    // Cooldown flag when count ceiling is hit
    bool CanThrow => _currentThrowCount < _maxThrowCount && _selectedAbility != null;


    void Awake()
    {
        throwPoint = transform;
        // initialise with projectiles's default Ability
        _selectedAbility = new AbilitySO[_projectiles.Count];
        for(int i = 0; i < _projectiles.Count; i++){
            _selectedAbility[i] = _projectiles[i].ability;
        }

        InitializeTimer();
    }

    void Update()=> _cooldownTimer.Tick(Time.deltaTime);

    public Vector3 CalculateThrowVelocity()
    {
        Vector3 forward = Camera.main.transform.forward;
        return forward * _throwForce + Vector3.up * upwardForce;
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
        if(!CanThrow) return;

        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile(_projectiles[currentIndex]);
        proj.transform.SetPositionAndRotation(throwPoint.position, Quaternion.identity);
        proj.SetAbility(_selectedAbility[currentIndex]);
        proj.Launch(CalculateThrowVelocity());
        
        
        _currentThrowCount ++;

        if(!CanThrow)
            _cooldownTimer.Start();

    }

    void InitializeTimer(){
        _cooldownTimer = new CountdownTimer(_cooldownDuration);
        _cooldownTimer.OnTimerStop = OnCooldownComplete;
    }

    void SetProjectileAbility(AbilitySO ability) => _selectedAbility[currentIndex] = ability;
    void ChangeIndex(int newIndex) {

        if(newIndex > _projectiles.Count - 1) return; // catch if index is out of bounds
        currentIndex = newIndex;

        string equipLabel = _projectiles[newIndex].DisplayName;
        // Debug.Log($"Equipped: {equipLabel}");
        Messenger.AddEquipMessage(equipLabel);
    }


    void OnCooldownComplete(){
        _currentThrowCount =0;
        Debug.Log("Throw cooldown ended");
    }
}
