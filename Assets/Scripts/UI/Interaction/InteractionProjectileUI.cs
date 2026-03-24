using UnityEngine;
using UnityEngine.UI;

public class InteractionProjectileUI : Singleton<InteractionProjectileUI>
{
    [SerializeField] Image _icon;
    Color _originalColor;
    Vector3 _originalScale;

    protected override void Awake()
    {
        base.Awake();
        _originalColor = _icon.color;
        _originalScale = _icon.transform.localScale;

        Hide();
    }

    public void SetSpriteIcon(Sprite icon)
    {
        _icon.sprite = icon;    
    }

    public void Show()
    {
        _icon.color = Color.yellow;
        _icon.transform.localScale = _originalScale * 1.2f;
        _icon.enabled = true;
    }

    public void Hide()
    {
        _icon.color = _originalColor;
        _icon.transform.localScale = _originalScale;
        _icon.sprite = null;
        _icon.enabled = false;
    }
}
