using System.Collections;
using TMPro;
using UnityEngine;

public class AbilitySelectionDisplay : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _display;
    [SerializeField] float _displayDuration = .6f;
    void Start() => _container.SetActive(false);

    void OnEnable() => RingMenu.OnAbilityIndicated += DisplaySelection;
    void OnDisable() => RingMenu.OnAbilityIndicated -= DisplaySelection;


    void DisplaySelection(string name, Color color) {
        StartCoroutine(DisplayRoutine(name, color));
    }


    IEnumerator DisplayRoutine(string name, Color color){
        _container.SetActive(true);
        _display.text = $"Selecting Ability:  {name}";
        _display.color = color;
        yield return new WaitForSeconds(_displayDuration);
        _display.text ="";
        _container.SetActive(false);
    }
}
