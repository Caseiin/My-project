using System.Collections;
using Mono.Cecil.Cil;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public AbilitySO Ability;
    void OnTriggerEnter(Collider other)
    {
        // Colour change
        var mesh = other.gameObject.GetComponent<MeshRenderer>();


        //effects aplication
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

    IEnumerator ColourChangeRoutine(MeshRenderer mesh)
    {
        yield return new WaitForSeconds(2.5f);
    }

}
