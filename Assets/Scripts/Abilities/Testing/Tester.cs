using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] AbilitySO ability;


    void OnTriggerEnter(Collider other)
    {
        var effectables = other.gameObject.GetComponents<IEffectable>();
        foreach(var effectable in effectables)
        {
            foreach ( var effect in ability.effects)
            {
                effect.Apply(effectable);
            }
        }
    }
}
