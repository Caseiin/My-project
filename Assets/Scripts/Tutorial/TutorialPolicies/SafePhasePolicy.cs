
public class SafePhasePolicy : ITutorialPhasePolicy
{
    // No failure, no hints
    public void OnStart(){}
    public void OnFailure(){}
    public void OnSuccess(){}
}