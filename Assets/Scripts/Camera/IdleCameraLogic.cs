using UnityEngine;

public class IdleCameraLogic : CameraLogic
{
    public IdleCameraLogic(PlayerController player) : base(player)
    {
    }

    public override void HandleLook()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
