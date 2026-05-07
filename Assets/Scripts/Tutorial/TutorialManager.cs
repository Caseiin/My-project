using System;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    readonly List<TutorialData> _tutorials = new();
    int _index = 0;
    bool _started = false;
    bool _idle = true;

    void Update()
    {
        if (_idle || _index >= _tutorials.Count) return;
        _tutorials[_index].Tick(Time.deltaTime);
    }

    public void AddTutorial(TutorialData tutorial)
    {
        if (_tutorials.Contains(tutorial)) return;
        _tutorials.Add(tutorial);
        if (!_started) TryStart();
        else if (_idle) StartStep(_index);
    }

    public void ReportFailure()
    {
        if (_idle || _index >= _tutorials.Count) return;
        _tutorials[_index].ReportFailure();
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
            _idle = true;
            Debug.Log("Tutorial sequence complete.");
            return;
        }

        _index = index;
        _idle  = false;

        var tut = _tutorials[index];
        tut.StartTutorial();
        tut.Bind(() => StartStep(_index + 1));
    }
}