using UnityEngine;

public class EnemyVulnerableState : IState
{
    readonly EnemyController _enemy;
    readonly GoapAgent _goapAgent;
    readonly MeshRenderer _renderer;
    readonly Material _originalMaterial;
    readonly Material _highlightMaterial;

    const float VulnerableDuration = 6f;
    const float FlashInterval = 0.2f;

    readonly CountdownTimer _countdownTimer;
    float _flashTimer;
    bool _isHighlighted;

    public bool IsVulnerabilityDone => _countdownTimer.IsFinished;

    public EnemyVulnerableState(EnemyController enemy, GoapAgent goapAgent, MeshRenderer renderer, Material highlightMaterial)
    {
        _enemy = enemy;
        _goapAgent = goapAgent;
        _renderer = renderer;
        _originalMaterial = renderer.material;
        _highlightMaterial = highlightMaterial;
        _countdownTimer = new CountdownTimer(VulnerableDuration);

        _countdownTimer.OnTimerStart += () => _enemy.IsMovementBlocked = true;
        _countdownTimer.OnTimerStop += OnExit;
    }

    public void OnEnter()
    {
        Debug.Log("Enemy in vulnerable state!");
        _enemy.Health.IsInvulnerable = false;
        _flashTimer = 0f;
        _isHighlighted = false;
        _countdownTimer.Start();
    }

    public void Update()
    {
        _countdownTimer.Tick(Time.deltaTime);

        _flashTimer -= Time.deltaTime;
        if (_flashTimer <= 0f)
        {
            _isHighlighted = !_isHighlighted;
            _renderer.material = _isHighlighted ? _highlightMaterial : _originalMaterial;
            _flashTimer = FlashInterval;
        }
    }

    public void FixedUpdate() { }

    public void OnExit()
    {
        _enemy.IsMovementBlocked = false;
        _enemy.Health.IsInvulnerable = true;
        _renderer.material = _originalMaterial; // always restore on exit
    }
}