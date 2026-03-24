using UnityEngine;
using UnityEngine.Pool;

public class EffectPopUpManager : Singleton<EffectPopUpManager>
{
    [SerializeField] GameObject _effectpopUp;
    [SerializeField] GameObject _effectPopUpPrefab;
    [SerializeField] Transform _uiParent;
    [SerializeField] float immediateDuration = 0.3f; 


    ObjectPool<EffectPopUp> _pool;

    protected override void Awake()
    {
        base.Awake();
        _pool = new ObjectPool<EffectPopUp>(CreateEffectPopUp, OnGet, OnRelease, OnDestroyPoolObject, false, 5, 7);
    }

    private EffectPopUp CreateEffectPopUp()
    {
        var go = Instantiate(_effectPopUpPrefab, _uiParent);
        return go.GetComponent<EffectPopUp>();
    }

    private void OnGet(EffectPopUp popup)
    {
        popup.gameObject.SetActive(true);
    }

    private void OnRelease(EffectPopUp popup)
    {
        popup.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(EffectPopUp popup)
    {
        Destroy(popup.gameObject);
    }

    public void DisplayEffect(Effect effect)
    {
        var popup = _pool.Get();
        popup.SetIcon(effect.EffectIcon);

        if(effect.Duration > 0f)
        {
            popup.DisplayTimed(effect.Duration, ()=> _pool.Release(popup));
        }
        else
        {
            popup.DisplayTimed(immediateDuration, () => _pool.Release(popup));
        }
    }
}
