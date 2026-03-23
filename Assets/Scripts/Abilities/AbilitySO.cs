using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/Ability")]
public class AbilitySO : ScriptableObject
{
    public Material abilityMaterial;
    [SerializeReference] public List<Effect> effects;
}
