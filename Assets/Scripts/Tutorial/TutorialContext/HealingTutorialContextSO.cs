using UnityEngine;

[CreateAssetMenu(fileName ="HealTutorial", menuName ="TutorialContext/Health")]
public class HealingTutorialContextSO : TutorialContextSO
{
    public override TutorialData Build(PlayerController player)
    {
        return new TutorialData.Builder(this)
            .WithPolicy(BuildPressurePolicy())
            .WithCompletionCondition(
                subscribe: cb => player.Health.OnHealing += cb,
                unsubscribe: cb => player.Health.OnHealing -= cb
            )
            .WithInitialCondition(()=> 
            {
                Debug.Log($"Started {TutorialName}");
                player.Health.TakeDamage(15);
            })
            .WithEndCondition(()=>Debug.Log($"Ended {TutorialName}"))
            .Build();
    }
}
