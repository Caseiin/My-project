using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
public class InputReader : ScriptableObject, InputSystem_Actions.IPlayerActions
{
    // Provides all necessary Input readings for other scripts to use

    public InputSystem_Actions Input;
    public Vector2 MoveDirection => Input.Player.Move.ReadValue<Vector2>();
    public Vector2 LookDirection => Input.Player.Look.ReadValue<Vector2>();
    public event Action onJumpPressed;

    public void EnableInputMap()
    {
        if (Input == null)
        {
            Input = new InputSystem_Actions();
            Input.Player.SetCallbacks(this);
            Input.Enable();
        }
    }

    public void DisableInputMap()
    {
        if (Input != null)
        {
            Input.Player.SetCallbacks(null);
            Input.Disable();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        onJumpPressed?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    }

    public void OnNext(InputAction.CallbackContext context)
    {

    }

    public void OnPrevious(InputAction.CallbackContext context)
    {

    }

    public void OnSprint(InputAction.CallbackContext context)
    {
    }
}
