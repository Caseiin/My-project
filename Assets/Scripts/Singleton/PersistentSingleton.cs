using UnityEngine;

public class PersistentSingleton<T> : MonoBehaviour where T : Component
{
    public bool AutoUnparentOnAwake = true;
    protected static T _instance;
    public static bool HasInstance => _instance != null;
    public static T TryGetInstance() => HasInstance? _instance : null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindAnyObjectByType<T>();
                if(_instance == null)
                {
                    var _gameObject = new GameObject(typeof(T).Name+ "Auto-Generated");
                    _instance = _gameObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        InitialiseSingleton();
    }
// Add persistent logic
    protected virtual void InitialiseSingleton()
    {
        if (!Application.isPlaying) return;

        if (AutoUnparentOnAwake)
        {
            transform.SetParent(null);
        }

        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
        }

        _instance = this as T;
    }
}
