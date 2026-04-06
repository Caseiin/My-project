using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public class InputReader : ScriptableObject,InputSystem_Actions.IUIActions,InputSystem_Actions.IPlayerActions
{
    // Provides all necessary Input readings for other scripts to use

    public InputSystem_Actions Input;
    public Vector2 MoveDirection{get;private set;}
    public Vector2 LookDirection{get;private set;}
    public bool IsAimming {get; private set;}

    public event Action OnShootTriggered;
    public event Action OnEscapeTriggered;
    public event Action OnAttackTriggered;
    public event Action OnResetTabTrigger;
    public event Action<ScreenType> OnMenuActivated;
    public event Action OnInteractTriggered;

    readonly PointerEventData _pointerEventData = new PointerEventData(EventSystem.current);
    readonly List<RaycastResult> _raycastResults = new List<RaycastResult>(); 


    public void EnableInputMap()
    {
        if (Input == null)
        {
            Input = new InputSystem_Actions();
            Input.Player.SetCallbacks(this);
            Input.UI.SetCallbacks(this);
            Input.Enable();
        }
    }

    public void DisableInputMap()
    {
        if (Input != null)
        {
            Input.Player.SetCallbacks(null);
            Input.UI.SetCallbacks(null);
            Input.Disable();
        }
    }

    // PlayerActions
    public void OnAttack(InputAction.CallbackContext context)
    {
        var pointer = Pointer.current;
        if (pointer != null && IsPointerOverUI(pointer.position.ReadValue())) return;
        if(context.performed) OnAttackTriggered?.Invoke();
    }


    public void OnCrouch(InputAction.CallbackContext context){}


    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started)
        OnInteractTriggered?.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context){}

    public void OnLook(InputAction.CallbackContext context)
    {
        LookDirection = context.ReadValue<Vector2>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        var pointer = Pointer.current;
        if (pointer != null && IsPointerOverUI(pointer.position.ReadValue())) return;

        if (context.started) OnShootTriggered?.Invoke();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.started)
        OnMenuActivated?.Invoke(ScreenType.PauseMenu);  

        OnEscapeTriggered?.Invoke();
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        // OnMenuActivated?.Invoke(ScreenType.MainMenu);
        if (context.started)
        OnResetTabTrigger?.Invoke();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        var pointer = Pointer.current;
        if (pointer != null && IsPointerOverUI(pointer.position.ReadValue())) return;

        IsAimming = context.action.IsPressed();
    }

    // UIActions
    public void OnNext(InputAction.CallbackContext context){}
    public void OnPrevious(InputAction.CallbackContext context){}
    public void OnSprint(InputAction.CallbackContext context){}
    public void OnNavigate(InputAction.CallbackContext context){}
    public void OnSubmit(InputAction.CallbackContext context){}
    public void OnCancel(InputAction.CallbackContext context){}
    public void OnPoint(InputAction.CallbackContext context){}
    public void OnClick(InputAction.CallbackContext context){}
    public void OnRightClick(InputAction.CallbackContext context){}
    public void OnMiddleClick(InputAction.CallbackContext context){}
    public void OnScrollWheel(InputAction.CallbackContext context){}
    public void OnTrackedDevicePosition(InputAction.CallbackContext context){}
    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context){}

    // Helper methods
    bool IsPointerOverUI(Vector2 screenPosition)
    {
        _pointerEventData.position =screenPosition;
        _raycastResults.Clear();
        EventSystem.current.RaycastAll(_pointerEventData, _raycastResults);
        return _raycastResults.Count > 0;
    }
}
