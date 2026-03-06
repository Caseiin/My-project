using UnityEngine;

public class Messenger
{
    string _message ="";
    public void AddEffectMessage(object effectname,string effect)
    {
        _message = $"{nameof(effectname)}: effect provided = {effect}";
    }

    public string DisplayMessage()
    {
        return _message;
    }
}
