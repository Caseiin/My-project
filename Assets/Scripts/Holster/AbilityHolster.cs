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
        _input.OnClickTriggered += SetAbility;
    }
    void OnDisable(){
        _input.OnAbilityHolsterTriggered -= SetMenuVisibility;
        _input.OnClickTriggered -= SetAbility;
    }

    void Update()
    {
        _accumulatedDirection = Vector2.zero;
        OnLook(_input.UIPointDirection);
        if(menuVisible && _accumulatedDirection != Vector2.zero)
            menu.FindMouseAngle(_accumulatedDirection);
    }

    void OnLook(Vector2 delta){
        if (!menuVisible) return;
        // Add deadzoning as the sensitivity is too high
        _accumulatedDirection += delta;

        if(_accumulatedDirection.magnitude > 1f)
            _accumulatedDirection.Normalize();
    }

    void SetMenuVisibility()
    {
        menuVisible = !menu.gameObject.activeSelf;
        menu.gameObject.SetActive(menuVisible);

        if(menuVisible){
            _input.Input.Player.Disable();
            _accumulatedDirection = Vector2.zero;
            ActionEventBus<CameraLogic>.Invoke(new SemiIdleCameraLogic(_player));
        }
        else
        {
            _input.Input.Player.Disable();
            ActionEventBus<CameraLogic>.Invoke(new FPSCameraLogic(_player));    
        } 
            
    }

    void SetAbility()
    {
        if (!menuVisible) return;

        menu.SelectAbilityElement();
        SetMenuVisibility(); //Close menu
    }
}
