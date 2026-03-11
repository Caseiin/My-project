using System;
using UnityEngine;

public static class Messenger
{
    public static event System.Action<String> OnEffectMessage;
    public static event System.Action<String> OnMessage;

    public static  void AddEffectMessage(string msg)
    {
        OnEffectMessage?.Invoke(msg);
    }

    public static  void AddMessage(string msg)
    {
        OnMessage?.Invoke(msg);
    }
}
