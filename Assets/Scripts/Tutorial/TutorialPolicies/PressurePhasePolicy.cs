using UnityEngine;
using System;

public class PressurePhasePolicy : ITutorialPhasePolicy
{
    // Shows hint after N number of failures
    readonly int _failuresThreshold;
    readonly float _inactivityTimeout;
    readonly Action _showHint;
    readonly Action _hideHint;

    int _failures;
    float _elapsed;
    bool _hintShown;
    bool _active;

    public PressurePhasePolicy(int threshold,float inactiveTime, Action showHint, Action hindHint){
        _failuresThreshold = threshold;
        _inactivityTimeout = inactiveTime;
        _showHint = showHint;
        _hideHint = hindHint;
    }

    public void OnStart(){
        _failures = 0;
        _elapsed = 0f;
        _hintShown = false;
        _active = true;
    }
    public void OnFailure()
    {
        if (_hintShown) return;
        _failures++;

        if(_failures >= _failuresThreshold)
            ShowHint();
    }

    public void OnTick(float deltatime){
        if(!_active || _hintShown) return;
        _elapsed += deltatime;
        if(_elapsed >= _inactivityTimeout)
            ShowHint();
    }

    void ShowHint(){
        _showHint?.Invoke();
        _hintShown = true;
    }
    public void OnSuccess(){
        _hideHint?.Invoke();
        _active =false;
    }
}