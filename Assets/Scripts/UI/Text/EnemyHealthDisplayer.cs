using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyHealthDisplayer : MonoBehaviour
{
    EnemyHealth _health;
    TextMeshPro _healthtext;
    void Awake()
    {
        _health = GetComponentInParent<EnemyHealth>();
        _healthtext = GetComponent<TextMeshPro>();
    }

    void DisplayHealthRestore(int health){
        string msg = $"+{health}";
        StartCoroutine(HealthDisplayCoroutine(msg));
    }
    void DisplayHealthDmg(int dmg){
        string msg = $"-{dmg}";
        StartCoroutine(HealthDisplayCoroutine(msg));
    }

    void OnEnable()
    {
        _health.OnHealthRestored += DisplayHealthRestore;
        _health.OnHealthTaken += DisplayHealthDmg;
    }

    void OnDisable()
    {
        _health.OnHealthRestored -= DisplayHealthRestore;
        _health.OnHealthTaken -= DisplayHealthDmg;
    }

    IEnumerator HealthDisplayCoroutine(string healthmsg)
    {
        _healthtext.text = healthmsg;
        yield return new WaitForSeconds(1f);
        _healthtext.text = "";
    }
}
