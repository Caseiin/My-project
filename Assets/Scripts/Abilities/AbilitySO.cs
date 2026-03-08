using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite AbilityEffectIcon;
    public Color AbilityColour;
    [SerializeReference] public List<Effect> effects;
}
