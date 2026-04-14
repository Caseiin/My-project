using UnityEngine;

public class SemiIdleCameraLogic : CameraLogic
{
    public SemiIdleCameraLogic(PlayerController player) : base(player)
    {
    }

    public override void HandleLook()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
