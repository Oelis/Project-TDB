using System;
using UnityEngine;
[Serializable]
public class ApplyPoison : DamageOverTimeEffect
{
    public override void Tick()
    {
        currentTarget.TakeDamage(damagePerTurn, DamageType.PoisonDamage);
    }
}
