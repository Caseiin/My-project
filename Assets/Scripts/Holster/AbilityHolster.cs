using UnityEngine;

public class AbilityHolster : MonoBehaviour
{
    [SerializeField] InputReader _input;
    [SerializeField] RingMenu menu;
    bool menuVisible;
    Vector2 _accumulatedDirection;
    PlayerController _player;

    void Start()
    {
        menu.gameObject.SetActive(false);
        _player = Registry<PlayerController>.GetFirst();
    }
    void OnEnable(){
        _input.OnAbilityHolsterTriggered += SetMenuVisibility;
        
    }
    void OnDisable(){
        _input.OnAbilityHolsterTriggered -= SetMenuVisibility;
    }

    void Update()
    {
        OnLook(_input.LookDirection);
        if(menuVisible && _accumulatedDirection != Vector2.zero)
            menu.FindMouseAngle(_accumulatedDirection);
    }

    void OnLook(Vector2 delta){
        if (!menuVisible) return;

        _accumulatedDirection += delta;

        if(_accumulatedDirection.magnitude > 1f)
            _accumulatedDirection.Normalize();
    }

    void SetMenuVisibility()
    {
        menuVisible = !menu.gameObject.activeSelf;
        menu.gameObject.SetActive(menuVisible);

        if(menuVisible){
            _accumulatedDirection = Vector2.zero;
            ActionEventBus<CameraLogic>.Invoke(new IdleCameraLogic(_player));
        }
        else 
            ActionEventBus<CameraLogic>.Invoke(new FPSCameraLogic(_player));
            
    }
}
