// DamageNumberUI.cs
using TMPro;
using UnityEngine;

public class DamageNumberUI : WorldSpaceUIFollower
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _lifetime = 1.2f;
    [SerializeField] float _floatSpeed = 2f;

    float _timer;
    float _floatOffset;

    public void SetDamage(int amount)
    {
        _text.text = $"-{amount.ToString()} hp";
    }

    // Only override OnInitialize if you need setup logic
    protected override void OnInitialize()
    {
        _timer = 0f;
        _floatOffset = 0f;
    }

    // Only put what's UNIQUE to damage numbers here
    protected override void OnTick()
    {
        _floatOffset += _floatSpeed * Time.deltaTime;

        // Modify the offset so base.UpdatePosition() uses it
        _offset = new Vector3(_offset.x, 2f + _floatOffset, _offset.z);

        _timer += Time.deltaTime;
        if (_timer >= _lifetime)
        {
            WorldSpaceUIManager.Instance.UnregisterFollower(this);
        }
    }

    // CleanUp is called by UnregisterFollower — base handles destroy
    public override void CleanUp()
    {
        base.CleanUp();
    }
}