using Mono.Cecil.Cil;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public AbilitySO Ability;
    void OnTriggerEnter(Collider other)
    {
        var effectables = other.gameObject.GetComponents<IEffectable>();
        foreach(var effectable in effectables)
        {
            Debug.Log($"effectable components: {effectable}");
            foreach ( var effect in Ability.effects)
            {
                Debug.Log($"effects: {effect}");
                effect.Apply(effectable);
            }
        }
    }

}
