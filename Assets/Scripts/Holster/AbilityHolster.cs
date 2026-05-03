using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolster : MonoBehaviour
{
    [SerializeField] InputReader _input;
    [SerializeField] RingMenu menu;
    bool menuVisible;
    PlayerController _player;

    void Start()
    {
        menu.gameObject.SetActive(false);
        _player = Registry<PlayerController>.GetFirst();
    }
    void OnEnable(){
        _input.OnMouseWheelScrolled += HandleScroll;
        _input.OnClickTriggered += SetAbility;
    }
    void OnDisable(){

        _input.OnMouseWheelScrolled -= HandleScroll;
        _input.OnClickTriggered -= SetAbility;
    }


    void HandleScroll(int step)
    {
        _player.SetCameraLogic(new IdleCameraLogic(_player));
        menu?.gameObject.SetActive(true);
        menu?.StepSelection(step);
    }

    void CloseMenu(){
        menu?.gameObject.SetActive(false);
        _player.SetCameraLogic(new FPSCameraLogic(_player));
    } 


    void SetAbility()
    {
        if(!menu.gameObject.activeSelf) return;
        Debug.Log($"SetAbility called. menuVisible: {menuVisible}");

        Debug.Log($"Calling SelectAbilityElement");
        menu.SelectAbilityElement();
        CloseMenu();//Close menu
    }
}
