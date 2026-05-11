using UnityEngine;

public class TutorialInstaller
{
    readonly PlayerController _player;

    public TutorialInstaller(PlayerController player = null){
        _player = player;
    }

    public void Install(BaseState state, TutorialContextSO context){
        if(_player == null){
            Debug.Log("Player is not assigned to TutorialIntstaller or is null");
            return;
        }

        var policy = new PressurePhasePolicy(
            threshold:  context.failure,
            inactiveTime: context.inactivityTimeout,
            showHint: () => Debug.Log(context.Hints),
            hindHint: () => Debug.Log("Hint hidden")
        );

        var tutorial = new TutorialData.Builder(context)
            .WithPolicy(policy)
            .WithCompletionCondition(
                subscribe:   cb => state.OnTutorialCompleted += cb,
                unsubscribe: cb => state.OnTutorialCompleted -= cb)
            .WithInitialCondition(() => Debug.Log($"Started: {context.TutorialName}"))
            .WithEndCondition(()     => Debug.Log($"Complete: {context.TutorialName}"))
            .Build();

        TutorialManager.Instance.AddTutorial(tutorial);
    }
}
