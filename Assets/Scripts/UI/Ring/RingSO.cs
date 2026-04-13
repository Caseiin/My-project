using UnityEngine;

[CreateAssetMenu(fileName = "RingSO", menuName = "RingMenu/Ring",order = 1)]
public class RingSO : ScriptableObject
{
    public RingElementSO[] Elements;
}
