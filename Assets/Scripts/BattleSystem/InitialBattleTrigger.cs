using UnityEngine;
using UnityEngine.Events;

public class InitialBattleTrigger : MonoBehaviour
{
    Collider _collider;
    public UnityEvent intialBattle;

    void Start(){
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            intialBattle?.Invoke();
            Destroy(gameObject);
        }
    }
}
