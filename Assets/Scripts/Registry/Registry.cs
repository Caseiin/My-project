using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate T SelectionStrategy<T>(IEnumerable<T> items); 
public delegate IEnumerable<T> FilterStrategy<T>(IEnumerable<T> items); 


public static class Registry<T> where T : class
{

    static readonly HashSet<T> items = new();

    public static bool TryAdd(T item)
    {
        return item != null & items.Add(item);
    }

    public static bool Remove(T item)
    {
        return items.Remove(item);
    }

    public static T Get(SelectionStrategy<T> strategy) => strategy(items);
    public static T GetFirst()
    {
        return items.FirstOrDefault();
    }
    public static IEnumerable<T> GetMany(FilterStrategy<T> filter) => filter(items);
}
