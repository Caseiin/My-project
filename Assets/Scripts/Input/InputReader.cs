using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject,InputSystem_Actions.IUIActions,InputSystem_Actions.IPlayerActions
{
    // Provides all necessary Input readings for other scripts to use

    public InputSystem_Actions Input;
    public Vector2 MoveDirection{get;private set;}
    public Vector2 LookDirection{get;private set;}

    public event Action OnShootTriggered;
    public event Action OnEscapeTriggered;


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

    public void OnAttack(InputAction.CallbackContext context){}


    public void OnCrouch(InputAction.CallbackContext context){}


    public void OnInteract(InputAction.CallbackContext context){}

    public void OnJump(InputAction.CallbackContext context){}

    public void OnLook(InputAction.CallbackContext context)
    {
        LookDirection = context.ReadValue<Vector2>();
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }


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

    public void OnShoot(InputAction.CallbackContext context)
    {
        OnShootTriggered?.Invoke();
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        OnEscapeTriggered?.Invoke();
    }
}
