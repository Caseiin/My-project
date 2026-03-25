using UnityEngine;

public interface IFactionMember
{
    public FactionType Faction {get;}
}

public enum FactionType
{
    Player,
    Enemy,
    Neutral
}