using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="AimTutorial", menuName ="TutorialContext/ Aim")]
public class AimTutorialContextSO : TutorialContextSO
{
    public override TutorialData Build(PlayerController player)
    {
        return new TutorialData.Builder(this)
            .WithPolicy(BuildPressurePolicy())
            .WithCompletionCondition(
                subscribe: cb => player.Input.OnAimStarted += cb,
                unsubscribe: cb => player.Input.OnAimStarted -= cb
            )
            .WithInitialCondition(()=> Debug.Log($"Started {TutorialName}"))
            .WithEndCondition(()=>Debug.Log($"Ended {TutorialName}"))
            .Build();
    }
}
