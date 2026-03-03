using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] AbilitySO ability;


    void OnTriggerEnter(Collider other)
    {
        foreach(var effect in ability.effects)
        {
            if (other.gameObject.TryGetComponent<IEffectable>(out IEffectable effectable)){
                effect.Apply(effectable);
            }
        }
    }
}
