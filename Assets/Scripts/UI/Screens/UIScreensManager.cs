using System.Collections.Generic;
using UnityEngine;

public class UIScreensManager : Singleton<UIScreensManager>
{
    [SerializeField] List<UIScreens> _screens;
    [SerializeField] InputReader _input;
    private Dictionary<ScreenType,UIScreens> _screensMap;
    private ScreenType? _currentScreen;
    PlayerController _player;

    protected override void Awake()
    {
        base.Awake();
        InitializeDictionary();
        _input.EnableInputMap();
    }

    void Start()
    {
        _player = Registry<PlayerController>.GetFirst();
    }

    void InitializeDictionary()
    {
        _screensMap = new Dictionary<ScreenType, UIScreens>();

        foreach(var screen in _screens)
        {
            _screensMap.Add(screen.ScreenType, screen);
        }
    }

    public void ShowScreen(ScreenType type)
    {
        HideAllScreens();
        _screensMap[type].Show();
        _player?.SetCameraLogic(new IdleCameraLogic(_player));
    }

    public void HideAllScreens()
    {
        // hides all screens
        foreach(var screen in _screensMap.Values) screen.Hide();
        _player?.SetCameraLogic(new FPSCameraLogic(_player));

    }


    public void ToggleScreen(ScreenType type)
    {
        if (_currentScreen == type)
        {
            HideAllScreens();
            _currentScreen = null;
        }
        else
        {
            ShowScreen(type);
            _currentScreen = type;
        }
    }
    void OnEnable()
    {
        _input.OnMenuActivated += ToggleScreen;
    }

    void OnDisable()
    {
        _input.OnMenuActivated -= ToggleScreen;
    }
}
