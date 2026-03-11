using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PanelController : MonoBehaviour
{
    [SerializeField] InputReader _inputReader;
    [SerializeField] GameObject _quitPanel;
    bool isQuitPanelopen = false;
    PlayerController _player;

    void Awake()
    {
        _inputReader.EnableInputMap();
        _player = FindFirstObjectByType<PlayerController>();
    }

    void ToggleQuitPanel()
    {
        isQuitPanelopen = !isQuitPanelopen;

        _quitPanel.SetActive(isQuitPanelopen);

        if (isQuitPanelopen)
        {
            // Switch to UI camera
            _player.SetCameraLogic(new IdleCameraLogic(_player));
        }
        else
        {
            // Switch back to gameplay
            _player.SetCameraLogic(new FPSCameraLogic(_player));
        }
    }

    void OnEnable()
    {
        _inputReader.OnEscapeTriggered += ToggleQuitPanel;
    }

    void OnDisable()
    {
        _inputReader.OnEscapeTriggered -= ToggleQuitPanel;
    }
}

// public enum PanelType
// {
//     Tutorial,
//     PlayerHUD,
//     Menu,
//     Quit,
//     DuckShootingGame,
//     BasketBallGame,
//     UnAvailableGame,
//     Dialogue
// }

// [System.Serializable]
// public struct panelLibrary
// {
//     public PanelType panelName;
//     public GameObject panelObject;
//     public bool visibilityState;
// }
