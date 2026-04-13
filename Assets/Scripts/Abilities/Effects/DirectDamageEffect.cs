using System;
using Enums;
using Interfaces;
using Units;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public abstract class DirectDamageEffect : IEffect
    {
        public int damageAmount = 10;
        
        public void Apply(UnitBrain source, UnitBrain target)
        {
            //if(CritPolicy.GetOrCreate().Roll(source.Stats.CriChance)) damageAmount = damageAmount * source.Stats.Strength;
            Debug.Log($"[DamageEffect] {source.GetSource().name} attacked {target.GetSource().name} for {damageAmount} {GetType()} damage.");
            target.TakeDamage(damageAmount, GetType());
            
        }
        
        public void Cleanup()
        {
        }
        
    }
}