using UnityEngine;

[CreateAssetMenu(fileName = "ButtonDataSO", menuName = "UI/ButtonData")]
public class ButtonDataSO : ScriptableObject
{
    public Sprite Default;
    public Sprite Hover;
    public Sprite Pressed;
    public Sprite Disabled;

}
