using System.Collections.Generic;
using UnityEngine;

public class UIScreensManager : Singleton<UIScreensManager>
{
    [SerializeField] List<UIScreens> _screens;
    private Dictionary<ScreenType,UIScreens> _screensMap;

    protected override void Awake()
    {
        base.Awake();
        InitializeDictionary();
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
        // hides all screens
        foreach(var screen in _screensMap.Values) screen.Hide();

        _screensMap[type].Show();
    }
}
