using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyNameDisplay : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _display;
    [SerializeField] float _displaySeconds = 3f;

    void DisplayEnemy(string name){

    }

    IEnumerator DisplayCoroutine(string name){
        _container.SetActive(true);
        _display.text = $" New enemy approaching: {name}";
        yield return  new WaitForSeconds(_displaySeconds);
        _display.text = "";
        _container.SetActive(true);

    }
}
