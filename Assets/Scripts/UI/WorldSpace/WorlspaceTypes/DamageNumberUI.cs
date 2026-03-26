using TMPro;
using UnityEngine;

public class DamageNumberUI : WorldSpaceUIFollower
{
    [SerializeField] TextMeshProUGUI _text;

    float _lifetime = 1.2f;
    float _timer;
    float _floatSpeed = 40f;

    public void SetDamage(int amount)
    {
        _text.text = amount.ToString();
    }

    public override void Tick(Camera cam)
    {
        base.Tick(cam); 

        // float upward (screen space)
        transform.position += Vector3.up * _floatSpeed * Time.deltaTime;

        // lifetime
        _timer += Time.deltaTime;

        if (_timer >= _lifetime)
        {
            WorldSpaceUIManager.Instance.UnregisterFollower(this);
        }
    }

    public override void CleanUp()
    {
        _timer = 0;
        base.CleanUp();
    }
}
