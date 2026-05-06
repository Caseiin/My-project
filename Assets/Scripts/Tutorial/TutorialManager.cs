using System;
using UnityEngine;
using System.Collections.Generic;

public class TutorialManager : Singleton<TutorialManager>
{
    readonly List<TutorialData> _tutorials = new();
    int _index = 0;
    bool _started = false;

    // Called by anything that adds a tutorial — safe to call multiple times
    public void AddTutorial(TutorialData tutorial)
    {
        if (_tutorials.Contains(tutorial)) return;
        _tutorials.Add(tutorial);

        // If we haven't started yet, try now (handles late registration)
        if (!_started) TryStart();
    }

    void TryStart()
    {
        if (_tutorials.Count > 0)
        {
            _started = true;
            StartStep(0);
        }
    }

    void StartStep(int index)
    {
        if (index >= _tutorials.Count)
        {
            Debug.Log("Tutorial complete!");
            return;
        }

        _index = index;
        var tut = _tutorials[index];
        tut.StartTutorial();
        tut.Bind(() => StartStep(_index + 1)); // pass "go to next" as the callback
    }
}