using System.Collections;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
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
            ColourChangeRoutine(mesh);
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
        var originalMaterial = mesh.material;
        Debug.Log("Material changed for effected entity");
        var abilityMaterial = this.GetComponent<MeshRenderer>().material;
        mesh.material = abilityMaterial;
        yield return new WaitForSeconds(2.5f);
        mesh.material = originalMaterial;

    }

}
