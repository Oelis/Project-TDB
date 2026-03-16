using System;
using UnityEngine;

[Serializable]
class KnockbackEffect : AbilityEffect
{
    public float force;
    public override void Execute(GameObject caster, GameObject target)
    {
        Debug.Log($"{caster.name} knocked back {target.name} with force {force}");
    }
}