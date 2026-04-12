using System.Collections.Generic;
using UnityEngine;

public class HolsterList<T> where T: AbilityProjectile
{
    public List<T> projectiles = new List<T>();
}
