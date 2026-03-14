using UnityEngine;

[CreateAssetMenu(fileName = "AttackStrategy", menuName = "Enemy/Attack")]
public abstract class AttackStrategy : ScriptableObject
{
    public abstract void Attack(EnemyController enemy);
}
