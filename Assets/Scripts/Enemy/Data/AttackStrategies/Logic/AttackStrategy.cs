using UnityEngine;

[CreateAssetMenu(fileName = "AttackStrategy", menuName = "Enemy/Attack")]
public abstract class AttackStrategy : ScriptableObject
{
    public virtual float CooldownDuration {get;} = 0f;
    protected CountdownTimer countdownTimer;
    
    public virtual void Initialize(EnemyController enemy) => countdownTimer = new CountdownTimer(CooldownDuration);
    public void Tick(float deltaTime) => countdownTimer.Tick(deltaTime);
    public abstract void Attack(EnemyController enemy);
    public virtual void ResetCount(){}

}
