using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all ability projectiles. Handles physics launch, lifetime,
/// AoE effect application, and visual feedback. Subclasses define impact range display.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class AbilityProjectile : MonoBehaviour
{
    [SerializeField] string _displayName;
    [SerializeField] protected GameObject Range;

    [Header("Config")]
    [SerializeField] AbilitySO _defaultAbility;
    public float MaxEffectRadius   = 5f;
    public float MaxLifeTimeDuration = 15f;


    [HideInInspector] public int PrefabInstanceID;
    public AbilitySO Ability => _defaultAbility;
    public string DisplayName => _displayName;
    public static Action<Color,float> OnPlayerEffectLanded;

    protected Rigidbody _rb;

    MaterialPropertyBlock _rangeMPB;
    MaterialPropertyBlock _projectileMPB;
    Renderer _rangeRenderer;
    Renderer _projectileRenderer;

    CountdownTimer _lifeTimeTimer;

    // Cached shader IDs — static so they are resolved once per type, not per instance
    static readonly int s_RangeColorID     = Shader.PropertyToID("_Color");
    static readonly int s_ProjectileColorID = Shader.PropertyToID("_BaseColor");

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        _projectileRenderer = GetComponent<Renderer>();
        _rangeRenderer = Range.GetComponent<Renderer>();


        _rangeMPB = new MaterialPropertyBlock();
        _projectileMPB = new MaterialPropertyBlock();

        _lifeTimeTimer = new CountdownTimer(MaxLifeTimeDuration);
        _lifeTimeTimer.OnTimerStop = ReturnToPool;
    }

    void Start()=> Range.SetActive(false);
    void Update() => _lifeTimeTimer.Tick(Time.deltaTime);

    public void SetAbility(AbilitySO ability)
    {
        _defaultAbility = ability;
        ApplyAbilityColor(ability.abilityMaterial.color);
    }

    public void Launch(Vector3 impulse)
    {
        _rb.isKinematic   = false;
        _rb.linearVelocity = Vector3.zero;
        _rb.AddForce(impulse, ForceMode.Impulse);

        _lifeTimeTimer.Start();
    }

    public void ResetPhysics()
    {
        _rb.isKinematic    = true;
        _rb.linearVelocity = Vector3.zero;
    }

    public virtual void ReturnToPool()
    {
        ResetPhysics();
        ResetRangeVisual();
        ProjectileManager.Instance.ReturnProjectile(this);
    }

    public abstract void ShowImpactRange();
    protected void Activate()
    {
        FindEffectablesInRange(out var playerTargets, out var otherTargets);

        var context = new EffectContext(transform);

        ApplyEffectsToTargets(playerTargets, context, notifyUI: true);
        ApplyEffectsToTargets(otherTargets,  context, notifyUI: false);

        ReturnToPool();
    }

    void ApplyEffectsToTargets(List<IEffectable> targets, EffectContext context, bool notifyUI)
    {
        foreach (var effectable in targets)
        {
            foreach (var effect in Ability.effects)
            {
                bool applied = effect.Apply(effectable, context);

                // Debug.Log($"[{name}] Effect: {effect.GetType().Name} | " +
                //       $"Target: {(effectable as MonoBehaviour)?.name} | " +
                //       $"Applied: {applied} | NotifyUI: {notifyUI} | " +
                //       $"Duration: {effect.Duration}");

                if (applied && notifyUI)
                {
                    Debug.Log($"[{name}] Invoking popup for: {effect.GetType().Name}");
                    EffectPopUpManager.Instance.DisplayEffect(effect);
                    Messenger.AddEffectMessage(effect.Message);
                }
            }
        }
    }

    void FindEffectablesInRange(out List<IEffectable> playerList, out List<IEffectable> otherList)
    {
        var playerBuffer = new HashSet<IEffectable>();
        var otherBuffer  = new HashSet<IEffectable>();
        var seen         = new HashSet<GameObject>();

        foreach (var col in Physics.OverlapSphere(transform.position, MaxEffectRadius))
        {
            var root = col.transform.root.gameObject;
            if (!seen.Add(root)) continue;

            foreach (var effectable in root.GetComponents<IEffectable>())
            {
                if (effectable is IPlayerEffectable)
                    playerBuffer.Add(effectable);
                else
                    otherBuffer.Add(effectable);
            }
        }

        playerList = new List<IEffectable>(playerBuffer);
        otherList  = new List<IEffectable>(otherBuffer);
    }

    void ApplyAbilityColor(Color color)
    {
        SetRendererColor(_rangeRenderer, _rangeMPB,s_RangeColorID,color);
        SetRendererColor(_projectileRenderer, _projectileMPB, s_ProjectileColorID, color);
    }

    void SetRendererColor(Renderer rend, MaterialPropertyBlock mpb, int propertyID, Color color)
    {
        rend.GetPropertyBlock(mpb);
        mpb.SetColor(propertyID, color);
        rend.SetPropertyBlock(mpb);
    }

    void ResetRangeVisual()
    {
        Range.transform.localScale = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, MaxEffectRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Range.transform.localScale.x / 2f);
    }
}