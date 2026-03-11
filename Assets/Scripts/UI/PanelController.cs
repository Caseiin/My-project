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
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _losePanel;

    bool isQuitPanelopen = false;
    PlayerController _player;
    PlayerHealth _playerhealth;

    void Awake()
    {
        _inputReader.EnableInputMap();
        _player = FindFirstObjectByType<PlayerController>();
        _playerhealth = FindFirstObjectByType<PlayerHealth>();
    }

    void ToggleQuitPanel()
    {
        isQuitPanelopen = !isQuitPanelopen;

        _quitPanel.SetActive(isQuitPanelopen);

        if (isQuitPanelopen)
        {
            // Switch to UI camera
            _player.SetCameraLogic(new IdleCameraLogic(_player));
            _player.IsMovementBlocked = true;
        }
        else
        {
            // Switch back to gameplay
            _player.SetCameraLogic(new FPSCameraLogic(_player));
        }
    }

    void EnableWinPanel()
    {
        _player.SetCameraLogic(new IdleCameraLogic(_player));
        _player.IsMovementBlocked = true;
        _winPanel.SetActive(true);
    }

    void EnableLosePanel()
    {
        _player.SetCameraLogic(new IdleCameraLogic(_player));
        _player.IsMovementBlocked = true;
        _losePanel.SetActive(true);
    }

    void OnEnable()
    {
        _inputReader.OnEscapeTriggered += ToggleQuitPanel;
        _playerhealth.OnDeath += EnableLosePanel;
        EnemyDeathState.OnEnemyDeath += EnableWinPanel;
    }

    void OnDisable()
    {
        _inputReader.OnEscapeTriggered -= ToggleQuitPanel;
        EnemyDeathState.OnEnemyDeath -= EnableWinPanel;
        _playerhealth.OnDeath -= EnableLosePanel;

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
