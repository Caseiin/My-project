using UnityEngine;

public class TimedEffectDisplay : IEffectIconDisplay
{
    float _duration;
    public TimedEffectDisplay(float duration)
    {
        _duration = duration;
    }
    public void DisplayEffectIcon(Sprite icon)
    {
    }
}
