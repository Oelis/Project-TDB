using System;
using UnityEngine;

[Serializable]
class DirectDamageEffect : AbilityEffect
{
    public int amount;
    
    public override void Execute(GameObject caster, GameObject target)
    {
        Debug.Log($"{caster.name} dealt {amount} damage to {target.name}");
    }
}

[Serializable]

class ApplyStatModifier : AbilityEffect
{
    public StatType statType;
    

    public override void Execute(GameObject caster, GameObject target)
    {
        throw new NotImplementedException();
    }
}