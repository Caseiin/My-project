using UnityEngine;
using UnityEngine.Pool;

public class EffectPopUpManager : Singleton<EffectPopUpManager>
{
    [SerializeField] GameObject _effectPopUpPrefab;
    [SerializeField] Transform _uiParent;


    ObjectPool<EffectPopUp> _pool;

    protected override void Awake()
    {
        base.Awake();
        _pool = new ObjectPool<EffectPopUp>(CreateEffectPopUp, OnGetfromPool, OnReleasefromPool, OnDestroyPoolObject, false, 5, 7);
    }

    EffectPopUp CreateEffectPopUp()
    {
        var go = Instantiate(_effectPopUpPrefab, _uiParent);
        return go.GetComponent<EffectPopUp>();
    }

    void OnGetfromPool(EffectPopUp popup)
    {
        popup.gameObject.SetActive(true);
    }

    void OnReleasefromPool(EffectPopUp popup)
    {
        popup.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(EffectPopUp popup)
    {
        Destroy(popup.gameObject);
    }

    public void DisplayEffect(Effect effect)
    {
        var popup = _pool.Get();
        popup.SetIcon(effect.EffectIcon);
        popup.SetColour(effect.EffectColour);
        
        if(effect.Duration > 0f)
        {
            popup.DisplayTimed(effect.Duration, ()=> _pool.Release(popup));
        }
        else
        {
            popup.DisplayImmediate(()=> _pool.Release(popup));
        }
    }
}
