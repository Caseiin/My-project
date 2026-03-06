using System;
using UnityEngine;

public static class Messenger
{
    public static event System.Action<String> OnMessage;
    public static  void AddEffectMessage(string msg)
    {
        OnMessage?.Invoke(msg);
    }


}
