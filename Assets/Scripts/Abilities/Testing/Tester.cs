using System.Collections;
using Mono.Cecil.Cil;
using Unity.VisualScripting;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public AbilitySO Ability;
    void OnCollisionEnter(Collision collision)
    {
        var effectables = collision.gameObject.GetComponents<IEffectable>();

        if (effectables.Length == 0)return;

        var mesh = collision.gameObject.GetComponent<MeshRenderer>();

        if(mesh != null) StartCoroutine(ColourChangeRoutine(mesh));

        foreach (var effectable in effectables)
        {
            foreach (var effect in Ability.effects)
            {
                effect.Apply(effectable);
            }
        }

        Destroy(gameObject);
    }


    IEnumerator ColourChangeRoutine(MeshRenderer mesh)
    {
        var originalMaterial = mesh.material;
        var abilityMaterial = this.GetComponent<MeshRenderer>().material;
        mesh.material = abilityMaterial;
        yield return new WaitForSeconds(2.5f);
        mesh.material = originalMaterial;

    }

}
