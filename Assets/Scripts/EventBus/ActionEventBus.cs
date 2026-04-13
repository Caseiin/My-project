using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEventBus<T>
{
    static readonly  List<Action<T>> listeners = new();

    public static void AddListener(Action<T> listener)
    {
        listeners.Add(listener);
    }

    public static void RemoveListener(Action<T> listener)
    {
        listeners.Remove(listener);
    }

    public static void Invoke(T arg)
    {
        foreach(var listener in listeners)
        {
            listener.Invoke(arg);
        }
    }
}
