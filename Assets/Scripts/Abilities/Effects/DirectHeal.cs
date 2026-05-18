using System;
using Enums;
using Interfaces;
using Policies;
using Units;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public class DirectHeal : IEffect
    {
        [SerializeField] private int healAmount = 10;
        public void Apply(UnitBrain source, UnitBrain target)
        {
            int finalHeal = healAmount;

            if (LuckPolicy.Create().Roll(source.Stats.CriticalChance))
            {
                finalHeal = Mathf.RoundToInt(finalHeal * source.Stats.CriticalDamageMultiplier/100);
            }
            
            Debug.Log($"[DirectHeal] {source.GetSource().name} healed {target.GetSource().name} for {finalHeal}.");
            target.Heal(finalHeal);
        }

        public void Cleanup()
        {
            
        }
    }
}