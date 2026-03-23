using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class UIButton : UIComponent
{
    public override void ApplyTheme(UIThemes theme)
    {
        base.ApplyTheme(theme);
        var _image = GetComponent<Image>();
        var _button = GetComponent<Button>();

        _button.targetGraphic = _image;
        _image.sprite = theme?.Button.ButtonDefault;

        _button.transition = Selectable.Transition.SpriteSwap;
        _button.spriteState = theme.Button.State;
    }
}
