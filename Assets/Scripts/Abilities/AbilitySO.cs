using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AbilitySO", menuName = "Scriptable Objects/Ability")]
public class AbilitySO : ScriptableObject
{
    public Material abilityMaterial;
    public string Name = null;

    void OnEnable(){
        if (String.IsNullOrEmpty(Name))
            Name = name;
    }

    [SerializeReference] public List<Effect> effects;
}
