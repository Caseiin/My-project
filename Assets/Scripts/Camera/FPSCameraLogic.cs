using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSCameraLogic : CameraLogic
{
    InteractiveProjectile _currentTarget;
    InteractiveProjectileUI _currentUI;
    LayerMask interactableLayer = LayerMask.GetMask("Interactable");

    public FPSCameraLogic(PlayerController player) : base(player)
    {
        SetUp();
    }

    public override void HandleLook()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Smooth input first
        _currentLook.x = Mathf.SmoothDamp(_currentLook.x, _player.Input.LookDirection.x, ref _lookVelocity.x, _player._lookSmoothTime);
        _currentLook.y = Mathf.SmoothDamp(_currentLook.y, _player.Input.LookDirection.y, ref _lookVelocity.y, _player._lookSmoothTime);

        // Apply sensitivity
        float mouseX = _currentLook.x * _player._sensitivity;
        float mouseY = _currentLook.y * _player._sensitivity;

        // Rotate body (yaw)
        _player.transform.Rotate(Vector3.up * mouseX);

        // Rotate head (pitch)
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -_player._verticalClamp, _player._verticalClamp);

        _player._headTransform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        RayCastViewAbility();
    }

    void RayCastViewAbility()
    {
        RaycastHit hit;
        Transform _head = _player._headTransform;
        
        if (Physics.Raycast(_head.position, _head.forward, out hit, _player.ViewRange,interactableLayer))
        {
            Debug.DrawRay(_head.position, _head.forward * hit.distance, Color.red);

            if (hit.collider.TryGetComponent(out InteractiveProjectile interactive))
            {
                if (_currentTarget != interactive)
                {
                    ClearTarget();

                    _currentTarget = interactive;
                    _currentTarget.EnableRange();
                    _currentUI  = WorldSpaceUIManager.Instance.Spawn(_currentTarget.interactivePrefab, _currentTarget.transform);
                    _currentUI.Show();    
                }

                return;
            }
        }

        ClearTarget();
    }



    void TriggerInteractiveProjectile()
    {
        if(_currentTarget != null)
        {
            _currentTarget.ToInteract();
        }
    }

    void ClearTarget()
    {
        if (_currentTarget != null)
        {
            _currentTarget.DisableRange();

            // Remove UI
            if (_currentUI != null)
            {
                _currentUI.CleanUp();
                _currentUI = null;
            }

            _currentTarget = null;
        }
    }

    void SetUp()
    {
        _player.Input.OnInteractTriggered += TriggerInteractiveProjectile;
    }

    void CleanUp()
    {
        _player.Input.OnInteractTriggered -= TriggerInteractiveProjectile;
    }

    ~FPSCameraLogic()
    {
        CleanUp();
    }
}
