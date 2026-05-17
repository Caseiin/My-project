using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName ="WeaponScrollTutorial", menuName ="TutorialContext/WeaponScroll")]
public class WeaponScrollContextSO : TutorialContextSO
{
    public override TutorialData Build(PlayerController player)
    {
        return new TutorialData.Builder(this)
            .WithPolicy(BuildPressurePolicy())
            .WithCompletionCondition(
                subscribe: cb => player.Input.OnWeaponScrollTriggered += cb,
                unsubscribe: cb => player.Input.OnWeaponScrollTriggered -= cb
            )
            .WithInitialCondition(()=> Debug.Log($"Started {TutorialName}"))
            .WithEndCondition(()=>Debug.Log($"Ended {TutorialName}"))
            .Build();
    }
}