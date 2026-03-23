using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class UIButton : UIComponent
{
    public ButtonType buttonType;
    public override void ApplyTheme(UIThemes theme)
    {
        base.ApplyTheme(theme);
        var _image = GetComponent<Image>();
        var _button = GetComponent<Button>();

        ButtonDataSO data = buttonType switch
        {
            ButtonType.Primary => theme.PrimaryButton,
            ButtonType.Secondary => theme.SecondaryButton,
            _ => theme.PrimaryButton
        };

        _button.targetGraphic = _image;
        _image.sprite = data.ButtonDefault;

        _button.transition = Selectable.Transition.SpriteSwap;
        _button.spriteState = data.State;
    }
}