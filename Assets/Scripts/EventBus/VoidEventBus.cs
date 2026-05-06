using System;
using System.Collections.Generic;

public static class VoidEventBus
{
    static readonly List<Action> _listeners = new();

    public static void AddListener(Action action){
        if(_listeners.Contains(action)) return;

        _listeners.Add(action);
    }

    public static void RemoveListener(Action action)
    {
        if(!_listeners.Contains(action)) return;
        _listeners.Remove(action);
    }

    public static void Invoke()
    {
        foreach(var listener in _listeners)
        {
            listener.Invoke();
        }
    }
}