using UnityEngine;
using System.Collections.Generic;

public class EnemyPower : IPowerUp
{
    AbilitySO _ability;
    EffectContext _effectContext;

    public EnemyPower(AbilitySO ability, EffectContext effectContext){
        _ability = ability;
        _effectContext = effectContext;
    }

    public void UsePower(){
        FindEffectablesInRange(out var playerList, out var otherList);
        ApplyEffectsToTargets(playerList, _effectContext, notifyUI: true);
        ApplyEffectsToTargets(otherList,  _effectContext, notifyUI: false);
        Debug.Log("PowerUsed!");
    }

    void ApplyEffectsToTargets(List<IEffectable> targets, EffectContext context, bool notifyUI)
    {
        foreach (var effectable in targets)
        {
            foreach (var effect in _ability.effects)
            {
                bool applied = effect.Apply(effectable, context);



                if (applied && notifyUI)
                {
                    EffectPopUpManager.Instance.DisplayEffect(effect);
                    Messenger.AddEffectMessage(effect.Message);
                    // OnPlayerEffectLanded?.Invoke(_ability.abilityMaterial.color, effect.Duration);
                }
            }
        }
    }

    void FindEffectablesInRange(out List<IEffectable> playerList, out List<IEffectable> otherList)
    {
        var playerBuffer = new HashSet<IEffectable>();
        var otherBuffer  = new HashSet<IEffectable>();
        var seen         = new HashSet<GameObject>();

        foreach (var col in Physics.OverlapSphere(_effectContext.SourcePosition.position, 10))
        {
            var root = col.transform.root.gameObject;
            if (!seen.Add(root)) continue;

            foreach (var effectable in root.GetComponents<IEffectable>())
            {
                if (effectable is IPlayerEffectable)
                    playerBuffer.Add(effectable);
                else
                    otherBuffer.Add(effectable);
            }
        }

        playerList = new List<IEffectable>(playerBuffer);
        otherList  = new List<IEffectable>(otherBuffer);
    }
}
