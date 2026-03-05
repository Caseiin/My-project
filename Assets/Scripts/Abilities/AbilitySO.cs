using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite AbilityEffectIcon;
    [SerializeReference] public List<Effect> effects;
}
