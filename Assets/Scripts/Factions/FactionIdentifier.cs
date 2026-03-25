using UnityEngine;

public class FactionIdentifier : MonoBehaviour, IFactionMember
{
    [SerializeField] FactionType _faction;
    public FactionType Faction => _faction;
}
