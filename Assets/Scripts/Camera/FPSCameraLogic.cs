using UnityEngine;

public class FPSCameraLogic : CameraLogic
{
    public FPSCameraLogic(PlayerController player) : base(player)
    {
    }

    public override void HandleLook()
    {
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
    }
}
