using UnityEngine;
using UnityEngine.InputSystem;

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
        // _input.OnAbilityHolsterTriggered += SetMenuVisibility;
        _input.OnMouseWheelScrolled += HandleScroll; //! there is an subscription
    }
    void OnDisable(){
        // _input.OnAbilityHolsterTriggered -= SetMenuVisibility;
        _input.OnMouseWheelScrolled -= HandleScroll;
    }

    void Update()
    {
        if (!menuVisible) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            SetAbility();


        // Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // Vector2 direction = _input.UIPointDirection - screenCenter;

        // // Vector2 direction = _input.MouseScrollDirection;
        // if (direction.sqrMagnitude > 100f) // small deadzone
        //     menu.FindMouseAngle(direction.normalized);

        // TODO: Fix the error with subscribing
    }

    void HandleScroll(int step)
    {
        menu.gameObject.SetActive(true);
        menu.StepSelection(step);
    }
    void SetMenuVisibility()
    {
        menuVisible = !menu.gameObject.activeSelf;
        menu.gameObject.SetActive(menuVisible);

        if(menuVisible){
            _input.Input?.Player.Disable();
            _accumulatedDirection = Vector2.zero;
            ActionEventBus<CameraLogic>.Invoke(new IdleCameraLogic(_player));
        }
        else
        {
            _input.Input?.Player.Enable(); // fixed
            ActionEventBus<CameraLogic>.Invoke(new FPSCameraLogic(_player));    
        } 
            
    }

    void SetAbility()
    {
        Debug.Log($"SetAbility called. menuVisible: {menuVisible}");
        if (!menuVisible) return;

        Debug.Log($"Calling SelectAbilityElement");
        menu.SelectAbilityElement();
        SetMenuVisibility(); //Close menu
    }
}
