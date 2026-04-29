using UnityEngine;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI levelTxt;

    void OnEnable()
    {
        BattleSystem.OnLevelChanged += ChangeLevel;
    }

    void OnDisable()
    {
        BattleSystem.OnLevelChanged -= ChangeLevel;
    }
    
    void ChangeLevel(int level)
    {
        levelTxt.text = $"level :{level}";
    }
}
