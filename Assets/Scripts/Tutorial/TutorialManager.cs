using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Pool;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] TutorialItem _itemPrefab;
    [SerializeField] Transform _container;
    [SerializeField] int _defaultCapacity = 2;
    [SerializeField] int _maxPoolSize = 8;

    IObjectPool<TutorialItem> _pool;
    PlayerController _player;
    TutorialItem _activeItem;
    readonly List<TutorialData> _tutorials = new();
    int _index = 0;
    bool _started = false;
    bool _idle = true;

    protected override void Awake()
    {
        base.Awake();
        _pool = new ObjectPool<TutorialItem>(
            createFunc:    CreateItem,
            actionOnGet:   OnGetFromPool,
            actionOnRelease: OnReleaseToPool,
            actionOnDestroy: OnDestroyPoolItem,
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize:         _maxPoolSize
        );
    }

    void Start(){
        _player = Registry<PlayerController>.GetFirst();
    }


    void Update()
    {
        if (_idle || _index >= _tutorials.Count) return;
        _tutorials[_index].Tick(Time.deltaTime);
    }

    public void AddTutorial(TutorialData tutorial)
    {
        if (_tutorials.Contains(tutorial)) return;
        _tutorials.Add(tutorial);
        if (!_started) TryStart();
        else if (_idle) StartStep(_index);
    }

    public void ReportFailure()
    {
        if (_idle || _index >= _tutorials.Count) return;
        _tutorials[_index].ReportFailure();
    }

    public void ShowHint(string hint){
        if(_activeItem != null) return;

        _activeItem = _pool.Get();
        _activeItem.Show(hint, onHide: ()=> {
            if(_activeItem == null) return;
            _pool.Release(_activeItem);
            _activeItem = null; 
            _player.IsMovementBlocked = false;
        });

        _player.IsMovementBlocked = true;
    }

    public void HideHint(){
        if (_activeItem == null) return;
        _activeItem.Hide();
    }



    void TryStart()
    {
        if (_tutorials.Count > 0)
        {
            _started = true;
            StartStep(0);
        }
    }

    void StartStep(int index)
    {
        if (index >= _tutorials.Count)
        {
            _idle = true;
            Debug.Log("Tutorial sequence complete.");
            return;
        }

        _index = index;
        _idle  = false;

        var tut = _tutorials[index];
        tut.StartTutorial();
        tut.Bind(() => StartStep(_index + 1));
    }

    // Pool Callbacks
    
    void OnDestroyPoolItem(TutorialItem item)
    {
        Destroy(item.gameObject);
    }

    void OnReleaseToPool(TutorialItem item)
    {
        item.gameObject.SetActive(false);
    }

    void OnGetFromPool(TutorialItem item)
    {
        item.transform.SetParent(_container);
        item.gameObject.SetActive(true);
    }

    TutorialItem CreateItem()
    {
        var item = Instantiate(_itemPrefab, _container);
        item.gameObject.SetActive(false);
        return item;
    }
}