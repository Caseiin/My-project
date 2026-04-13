using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AbilityProjectile : MonoBehaviour
{
    [HideInInspector] public int PrefabInstanceID;
    [SerializeField] protected GameObject Range;
    public float MaxEffectRadius = 5f;
    public AbilitySO ability;
    protected Rigidbody _rb;

    // Effect searching
    List<IEffectable> _playerEffectables;
    List<IEffectable> _otherEffectables;

    // Material Property block
    Material _projectileMat;
    MaterialPropertyBlock _rangeMBP;
    MaterialPropertyBlock _mbp;

    Renderer _rangeRenderer;
    Renderer _renderer;
    static readonly int RangeColorID = Shader.PropertyToID("_Color");
    static readonly int ColorID = Shader.PropertyToID("_BaseColor");


    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _projectileMat = GetComponent<Material>();
        _rangeRenderer = Range.GetComponent<Renderer>();
        _renderer = GetComponent<Renderer>();
        _rangeMBP = new();
        _mbp = new();

    }

    void Start()
    {
        Range.SetActive(false);
        var abilityColor =ability.abilityMaterial.color; 
        SetRangeColor(abilityColor);
        SetProjectileColor(abilityColor);   
    }

    public void Launch(Vector3 impulse)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(impulse, ForceMode.Impulse);
    }


    protected void Activate()
    {
        FindEffectablesWithinRange(out var _playerEffectables, out var _otherEffectables);

        // player
        foreach (var effectable in _playerEffectables)
        {
            foreach (var effect in ability.effects)
            {
                if (effect.Apply(effectable))
                {
                    EffectPopUpManager.Instance.DisplayEffect(effect);
                    Messenger.AddEffectMessage(effect.Message);
                }
            }
        }

        // other
        foreach (var effectable in _otherEffectables)
        {
            foreach (var effect in ability.effects)
            {
                effect.Apply(effectable);
            }
        }

        ReturnToPool();
    }

    public virtual void ReturnToPool()
    {
        _rb.linearVelocity = Vector3.zero;
        ResetRange();
        ProjectileManager.Instance.ReturnProjectile(this);
    }

    public abstract void ShowImpactRange();

    void SetRangeColor(Color color)
    {

        _rangeRenderer.GetPropertyBlock(_rangeMBP);
        _rangeMBP.SetColor(RangeColorID, color);
        _rangeRenderer.SetPropertyBlock(_rangeMBP);
    }

    void SetProjectileColor(Color color)
    {

        _renderer.GetPropertyBlock(_mbp);
        _mbp.SetColor(ColorID, color);
        _renderer.SetPropertyBlock(_mbp);
    }

    void ResetRange()
    {
        Range.transform.localScale = Vector3.zero;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MaxEffectRadius);
    }
    void FindEffectablesWithinRange(out List<IEffectable> playerList, out List<IEffectable> otherList)
    {
        var playerBuffer = new HashSet<IEffectable>();
        var otherBuffer = new HashSet<IEffectable>();

        // Track root GameObjects already processed to avoid double-hitting
        var seen = new HashSet<GameObject>();

        var colliders = Physics.OverlapSphere(transform.position, MaxEffectRadius);

        foreach (var col in colliders)
        {
            // Use the root GameObject as the unique key
            var root = col.transform.root.gameObject;
            if (!seen.Add(root)) continue; // already processed this enemy

            var effectables = root.GetComponents<IEffectable>();
            foreach (var e in effectables)
            {
                if (e is IPlayerEffectable)
                    playerBuffer.Add(e);
                else
                    otherBuffer.Add(e);
            }
        }

        playerList = new List<IEffectable>(playerBuffer);
        otherList = new List<IEffectable>(otherBuffer);
    }

    public void ResetPhysics()
    {
        _rb.isKinematic = true;
        _rb.linearVelocity = Vector3.zero;
    }
}