using UnityEngine;

public class EffectContext
{
    public Transform SourcePosition {get;} 
    public EffectContext(Transform position = null){
        SourcePosition = position;
    }
}
