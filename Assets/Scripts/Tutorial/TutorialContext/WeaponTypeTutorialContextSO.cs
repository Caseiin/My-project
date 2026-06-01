using UnityEngine;

[CreateAssetMenu(fileName ="WeaponTypeTutorial", menuName ="TutorialContext/WeaponType")]
public class WeaponTypeTutorialContextSO : TutorialContextSO
{
    public override TutorialData Build(PlayerController player)
    {
        return new TutorialData.Builder(this)
            .WithPolicy(BuildPressurePolicy())
            .WithCompletionCondition(
                subscribe: cb => player.Input.OnHotBarKeyPressed += cb,
                unsubscribe: cb => player.Input.OnHotBarKeyPressed -= cb
            )
            .WithInitialCondition(()=>Debug.Log($"Started {TutorialName}"))
            .WithEndCondition(()=>Debug.Log($"Ended {TutorialName}"))
            .Build();
    }
}