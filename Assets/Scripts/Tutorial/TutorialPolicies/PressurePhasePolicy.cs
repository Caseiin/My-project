
using System;

public class PressurePhasePolicy : ITutorialPhasePolicy
{
    // Shows hint after N number of failures
    readonly int _failuresThreshold;
    readonly Action _showHint;
    readonly Action _hideHint;
    int _failures;

    public PressurePhasePolicy(int threshold, Action showHint, Action hindHint){
        _failuresThreshold = threshold;
        _showHint = showHint;
        _hideHint = hindHint;
    }

    public void OnStart(){_failures = 0;}
    public void OnFailure()
    {
        _failures++;

        if(_failures >= _failuresThreshold)
            _showHint?.Invoke();
    }
    public void OnSuccess()=> _hideHint?.Invoke();
}