using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public static class EventBus
{
    static readonly Dictionary<Type, List<Delegate>> _subscribers = new();

    public static void Subscribe<T>(Action<T> handler) where T : IGameEvent
    {
        Type t = typeof(T);
        if(!_subscribers.TryGetValue(t, out List<Delegate> listeners))
        {
            listeners = new();
            _subscribers[t] = listeners;
        }
        listeners.Add(handler);
    }

    public static void UnSubscribe<T>(Action<T> handler) where T: IGameEvent
    {
        Type t = typeof(T);
        if (_subscribers.TryGetValue(t, out List<Delegate> listeners))
        {
            listeners.Remove(handler);

            if (listeners.Count == 0)
                _subscribers.Remove(t);
        }
    }

    public static void Publish<T>(T ev) where T: IGameEvent
    {
        Type t = typeof(T);
        if(_subscribers.TryGetValue(t, out List<Delegate> listeners))
        {
            Delegate[] delegates = listeners.ToArray();
            foreach(var item in delegates){
                ((Action<T>)item)(ev);
            }
        } 
    }
}
