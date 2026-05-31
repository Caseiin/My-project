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
    readonly Queue<TutorialData> _tutorials = new(); 
    TutorialData _current;
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
        if (_idle || _current== null) return;
        _current.Tick(Time.deltaTime);
    }

    public void AddTutorial(TutorialData tutorial)
    {
        _tutorials.Enqueue(tutorial);
        if(_idle) StartNext();
    }



    public void ReportFailure()
    {
        if(_idle || _current == null) return;
        _current.ReportFailure();
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

    void StartNext()
    {
        if (_tutorials.Count == 0)
        {
            _current = null;
            _idle = true;
            Debug.Log("Tutorial sequence complete.");
            return;
        }

        _idle = false;
        _current = _tutorials.Dequeue(); 
        _current.StartTutorial();
        _current.Bind(StartNext);
    }


    // Pool Callbacks
    
    void OnDestroyPoolItem(TutorialItem item) => Destroy(item.gameObject);
    void OnReleaseToPool(TutorialItem item) => item.gameObject.SetActive(false);
    void OnGetFromPool(TutorialItem item){
        item.transform.SetParent(_container);
        item.gameObject.SetActive(true);
    }

    TutorialItem CreateItem(){
        var item = Instantiate(_itemPrefab, _container);
        item.gameObject.SetActive(false);
        return item;
    }
}