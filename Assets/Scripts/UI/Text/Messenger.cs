using UnityEngine;

public class Messenger
{
    string _message ="";
    public void AddEffectMessage(string effectname,string effect)
    {
        _message = $"{effectname}: effect provided = {effect}";
    }

    public string DisplayMessage()
    {
        return _message;
    }
}
