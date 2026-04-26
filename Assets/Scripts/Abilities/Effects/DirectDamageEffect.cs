using System;
using Enums;
using Interfaces;
using Policies;
using Units;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public abstract class DirectDamageEffect : IEffect
    {
        [SerializeField] protected int damageAmount = 10;
        [SerializeField] protected bool canBeEvaded = true;
        [SerializeField] protected bool canBeBlocked = true;
        
        public abstract StatType ResistanceStat { get; }
        
        public void Apply(UnitBrain source, UnitBrain target)
        {
            int finaldamage = damageAmount;

            if (LuckPolicy.Create().Roll(source.Stats.CriticalChance))
            {
                finaldamage = Mathf.RoundToInt(finaldamage * source.Stats.CriticalDamageMultiplier/100);
            }
            
            Debug.Log($"[DamageEffect] {source.GetSource().name} attacked {target.GetSource().name} for {finaldamage} {GetType()} damage.");
            target.TakeDamage(finaldamage, GetType(), ResistanceStat, canBeEvaded, canBeBlocked);
            
        }
        
        public void Cleanup()
        {
        }
        
    }
}