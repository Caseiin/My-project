using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyNameDisplay : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _display;
    [SerializeField] TextMeshProUGUI _hint;

    [SerializeField] float _displaySeconds = 3f;

    void Start() => _container.SetActive(false);

    void OnEnable()=> BattleSystem.OnEnemySpawned += DisplayEnemy;
    void OnDisable()=> BattleSystem.OnEnemySpawned -= DisplayEnemy;


    void DisplayEnemy(string name, string hint){
        StartCoroutine(DisplayCoroutine(name,hint));
    }

    IEnumerator DisplayCoroutine(string name, string hint){
        _container.SetActive(true);
        _display.text = $"New Enemy Spawning:  {name}";
        _hint.text =$"Hint:{hint}"; 
        yield return  new WaitForSeconds(_displaySeconds);
        _display.text = "";
        _hint.text = "";
        _container.SetActive(false);

    }
}
