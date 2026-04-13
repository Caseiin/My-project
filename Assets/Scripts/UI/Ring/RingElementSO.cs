using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RingElementSO", menuName = "RingMenu/RingElement",order = 2)]
public class RingElementSO : ScriptableObject
{
    void OnEnable()
    {
        if (String.IsNullOrEmpty(Name))
            Name = name;
    }

    public string Name;
    public Sprite Icon;
    public AbilitySO Ability;
    public RingSO NextRing;
}
