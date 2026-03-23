using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ButtonDataSO", menuName = "UI/ButtonData")]
public class ButtonDataSO : ScriptableObject
{
    public Sprite ButtonDefault;
    public SpriteState State;

}
