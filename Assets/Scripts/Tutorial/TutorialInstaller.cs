using UnityEngine;

public class TutorialInstaller
{
    readonly PlayerController _player;

    public TutorialInstaller(PlayerController player = null){
        _player = player;
    }

    public void Install(TutorialContextSO context){
        if(_player == null){
            Debug.Log("Player is not assigned to TutorialIntstaller or is null");
            return;
        }

        var tutorial = context.Build(_player);
        TutorialManager.Instance.AddTutorial(tutorial);
    }
}
