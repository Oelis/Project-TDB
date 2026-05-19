using System;
using System.Collections.Generic;
using Enums;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public class DamageOverTimeEffect : EffectOverTime, ITickEffect, IFlippable
    {
        [SerializeField] protected int damagePerTurn;
        [ValueDropdown(nameof(GetAllowedDamageTypes))]
        public DamageType DamageType;

        private static IEnumerable<DamageType> GetAllowedDamageTypes()
        {
            var values = new List<DamageType>();
            foreach (DamageType d in Enum.GetValues(typeof(DamageType)))
            {
                if (d != DamageType.PhysicalDamage)
                    values.Add(d);
            }
            return values;
        }
        
        public override int MaxStackSize => 99;
        public override bool CanBeStacked => true;
        public override EOTType EOTType => EOTType.Debuff;
        public override object StackKey => (GetType(), DamageType);


        public void InitializeFlip(int damage, DamageType type, int duration)
        {
            damagePerTurn = damage;
            DamageType = type;
            _turnDuration = duration;
        }

        public EffectOverTime Flip()
        {
            HealOverTime flippedEffect = new HealOverTime();
            flippedEffect.InitializeFlip(damagePerTurn, _turnDuration);
            return flippedEffect;
        }

        public void Tick()
        {
            Debug.Log($"[{GetType().Name}] has Tick");
            CurrentTarget.TakeDamage(damagePerTurn, DamageType,false,false);
        }
    }
}