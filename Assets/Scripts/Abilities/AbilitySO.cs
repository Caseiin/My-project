using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/Ability")]
public class AbilitySO : ScriptableObject
{
    [SerializeReference] public List<Effect> effects;
}
