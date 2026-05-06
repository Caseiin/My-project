using System;
using System.Collections.Generic;

public static class ActionEventBus<T>{
    static readonly List<Action<T>> _listeners = new();

    public static void AddListener(Action<T> action){
        if(_listeners.Contains(action)) return;

        _listeners.Add(action);
    }

    public static void RemoveListener(Action<T> action)
    {
        if(!_listeners.Contains(action)) return;
        _listeners.Remove(action);
    }

    public static void Invoke(T arg)
    {
        foreach(var listener in _listeners)
        {
            listener.Invoke(arg);
        }
    }
}