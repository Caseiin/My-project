using System;

public interface ITutorialPhasePolicy
{
    void OnStart();
    void OnSuccess();
    void OnFailure();
    void OnTick(float deltatime);
}