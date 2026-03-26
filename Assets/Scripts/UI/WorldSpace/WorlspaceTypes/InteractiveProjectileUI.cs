using TMPro;
using UnityEngine;

public class InteractiveProjectileUI : WorldSpaceUIFollower
{
    [SerializeField] string _interactMessage;
    [SerializeField] TextMeshProUGUI _text;
    Vector3 _originalScale;
    Vector3 _newOffset = new Vector3(0,.1f,-1);

    void Awake()
    {
        _originalScale = transform.localScale;
    }
    void SetMessage()
    {
        _text.text = _interactMessage;
    }
    public void Show()
    {
        SetVisible(true);
        SetMessage();
    }

    protected override void UpdatePosition()
    {
        transform.position = _target.position + _newOffset;
    }
    public void Hide()
    {
        SetVisible(false);
    }

    public void Highlight()
    {
        transform.localScale = _originalScale * 1.05f;
    }

}
