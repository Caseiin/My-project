using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PopUpManager : Singleton<PopUpManager>
{
    [SerializeField] GameObject popUp;
    [SerializeField] int _maxPopups = 3;
    [SerializeField] Transform _gamePopUpContainer;
    [SerializeField] Transform _systemPopUpContainer;
    [SerializeField] float _popUpDuration = 3f;

    readonly List<GamePlayPopUp> activePopUps = new List<GamePlayPopUp>();
    ObjectPool<GamePlayPopUp> _pool;

    protected override void Awake()
    {
        base.Awake();
        _pool = new ObjectPool<GamePlayPopUp>(
            CreatePopup,
            OnGetFromPool,
            OnReleaseToPool,
            OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity: 5,
            maxSize: 10
        );
    }


    public void AddGamePlayPopUp(string msg, Sprite icon)
    {
        GamePlayPopUp popup = _pool.Get();

        popup.transform.SetParent(_gamePopUpContainer);
        popup.SetUp(msg, icon);

        activePopUps.Add(popup);

        // Limit active popups
        if (activePopUps.Count > _maxPopups)
        {
            GamePlayPopUp oldest = activePopUps[0];
            activePopUps.RemoveAt(0);
            _pool.Release(oldest);
        }

        StartCoroutine(ReturnToPoolAfterTime(popup));
    }

    private GamePlayPopUp CreatePopup()
    {
        GameObject obj = Instantiate(popUp, _gamePopUpContainer);
        return obj.GetComponent<GamePlayPopUp>();
    }

    private void OnGetFromPool(GamePlayPopUp popup)
    {
        popup.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(GamePlayPopUp popup)
    {
        popup.CleanUp();
        popup.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(GamePlayPopUp popup)
    {
        Destroy(popup.gameObject);
    }

    private IEnumerator ReturnToPoolAfterTime(GamePlayPopUp popup)
    {
        yield return new WaitForSeconds(_popUpDuration);

        if (activePopUps.Contains(popup))
        {
            activePopUps.Remove(popup);
            _pool.Release(popup);
        }
    }
}
