using UnityEngine;

[CreateAssetMenu(fileName ="EnemySetUp", menuName ="Enemy/SetUp")]
public class EnemySetUpSO : ScriptableObject
{
    public int Health = 0;
    public BaseActionSetup Actions;
    public BaseBeliefSetUps Beliefs;
    public BaseGoalsSetup Goals;
    
    public AbilitySO Reward = null;
}