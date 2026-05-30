using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileThrow : MonoBehaviour
{
    [Header("Input Dependency")]
    [SerializeField] InputReader _input;
    [Header("Throw Info")]
    [SerializeField] List<AbilityProjectile> _projectiles;
    [SerializeField] float _throwForce = 10f;
    [SerializeField] float upwardForce = 2f;

    [Header("Throw Limits")]
    [SerializeField] int _maxAbilityThrowCount  = 3;   // per-ability limit
    [SerializeField] float _abilityCooldownDuration = 8f; // per-ability cooldown
    [SerializeField] int _impactThrowLimit      = 2;   // reduced limit when interactive available
    public float ThrowForce => _throwForce;
    public static event Action<string, float, Color> OnCooldownActive;

    Dictionary<AbilitySO, AbilityCooldownState> _abilityCooldowns = new();

    int currentIndex;
    AbilitySO[] _selectedAbility;
    List<AbilitySO> _abilityKeys = new();
    Transform throwPoint;

    void Awake()
    {
        throwPoint = transform;
    }

    void Start()
    {
        // initialise with projectiles's default Ability
        _selectedAbility = new AbilitySO[_projectiles.Count];
        for(int i = 0; i < _projectiles.Count; i++){
            _selectedAbility[i] = _projectiles[i].Ability;
            RegisterAbility(_selectedAbility[i]);
        }
        
    }

    void Update(){
        foreach (var ability in _abilityKeys)
            _abilityCooldowns[ability].Timer.Tick(Time.deltaTime);
    }

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

    void RegisterAbility(AbilitySO ability){
        if(_abilityCooldowns.ContainsKey(ability)) return; //Already Registered Guard case

        var timer = new CountdownTimer(_abilityCooldownDuration);
        timer.OnTimerStop = ()=> OnAbilityCooldownComplete(ability);

        var cooldownState = new AbilityCooldownState{
            ThrowCount =  0,
            Timer = timer
        };

        _abilityCooldowns[ability] = cooldownState;
        _abilityKeys.Add(ability);
    }

    public void Throw()
    {
        // TODO: Add logic to limit throwing whether that be a cooldown or a throw amount limit with the changing back to the next ability
        // TODO: Add  greater limiting logic for ImpactGrenades to prioritize interactive projectiles
        var ability = _selectedAbility[currentIndex];
        var projType = _projectiles[currentIndex];

        if(!CanThrowAbility(ability,projType)) return;

    
        AbilityProjectile proj = ProjectileManager.Instance.GetProjectile(_projectiles[currentIndex]);
        proj.transform.SetPositionAndRotation(throwPoint.position, Quaternion.identity);
        proj.SetAbility(_selectedAbility[currentIndex]);
        proj.Launch(CalculateThrowVelocity());
        
        // Debug.Log($"Prjectile ability set to {_selectedAbility[currentIndex]}");
        
        var state = _abilityCooldowns[ability];
        state.ThrowCount++;
        _abilityCooldowns[ability] = state;

        int limit = GetThrowLimit(projType);
        if(state.ThrowCount >= limit){
            Debug.Log("Projectile Cooldown");
            var color = ability.effects[0].EffectColour;
            OnCooldownActive?.Invoke($"{ability.Name} cooldown", _abilityCooldownDuration, color);
            _abilityCooldowns[ability].Timer.Start();
        }

    }


    void SetProjectileAbility(AbilitySO ability){
        _selectedAbility[currentIndex] = ability;
        RegisterAbility(ability);
    }
    void ChangeIndex(int newIndex) {

        if(newIndex > _projectiles.Count - 1) return; // catch if index is out of bounds
        currentIndex = newIndex;

        string equipLabel = _projectiles[newIndex].DisplayName;
        Messenger.AddEquipMessage(equipLabel);
    }

    void OnAbilityCooldownComplete(AbilitySO ability){
        if(!_abilityCooldowns.TryGetValue(ability, out var state)) return;

        state.ThrowCount = 0;
        _abilityCooldowns[ability] = state;
        Debug.Log($"[Projectile Throw] Cooldown ended  for {ability.name}");
    }

    bool CanThrowAbility(AbilitySO ability, AbilityProjectile projType){
        if (!_abilityCooldowns.TryGetValue(ability, out var state)) return false;
        if(state.Timer.IsRunning) return false;

        int limit = GetThrowLimit(projType);
        return state.ThrowCount < limit;
    }

    int GetThrowLimit(AbilityProjectile projType)
    {
        bool isImpact =  projType is ImpactExplosionProjectile;
        bool hasInteractive = _projectiles.Exists(p => p is InteractiveProjectile);

        return (isImpact && hasInteractive) ? _impactThrowLimit : _maxAbilityThrowCount;
    }
}
