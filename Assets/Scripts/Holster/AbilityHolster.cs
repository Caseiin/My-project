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
        _input.OnMouseWheelScrolled += HandleScroll;
        menu.onSelectedAbility += CloseMenu;
    }
    void OnDisable(){
        // _input.OnAbilityHolsterTriggered -= SetMenuVisibility;
        _input.OnMouseWheelScrolled -= HandleScroll;
        menu.onSelectedAbility -= CloseMenu;
    }


    void HandleScroll(int step)
    {
        menu?.gameObject.SetActive(true);
        menu?.StepSelection(step);
    }

    void CloseMenu()=> menu?.gameObject.SetActive(false);


    void SetAbility()
    {
        Debug.Log($"SetAbility called. menuVisible: {menuVisible}");

        Debug.Log($"Calling SelectAbilityElement");
        menu.SelectAbilityElement();
        CloseMenu();//Close menu
    }
}
