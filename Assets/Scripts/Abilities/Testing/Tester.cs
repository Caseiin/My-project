using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField] AbilitySO ability;
    void OnTriggerEnter(Collider other)
    {
        var effectables = other.gameObject.GetComponents<IEffectable>();
        foreach(var effectable in effectables)
        {
            Debug.Log($"effectable components: {effectable}");
            foreach ( var effect in ability.effects)
            {
                Debug.Log($"effects: {effect}");
                effect.Apply(effectable);
            }
        }
    }
}
