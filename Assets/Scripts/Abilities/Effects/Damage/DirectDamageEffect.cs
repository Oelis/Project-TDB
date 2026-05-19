using System;
using Enums;
using Interfaces;
using Policies;
using Units;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public class DirectDamageEffect : IEffect
    {
        [SerializeField] private int damageAmount = 10;
        [SerializeField] private bool canBeEvaded = true;
        [SerializeField] private bool canBeBlocked = true;
        [SerializeField] private DamageType damageType;
        
        public void Apply(UnitBrain source, UnitBrain target)
        {
            int finaldamage = damageAmount;

            if (LuckPolicy.Create().Roll(source.Stats.CriticalChance))
            {
                finaldamage = Mathf.RoundToInt(finaldamage * source.Stats.CriticalDamageMultiplier/100);
            }
            
            Debug.Log($"[DamageEffect] {source.GetSource().name} attacked {target.GetSource().name} for {finaldamage} {damageType} damage.");
            target.TakeDamage(finaldamage, damageType, canBeEvaded, canBeBlocked);
            
        }
        
        public void Cleanup()
        {
        }
        
    }
}