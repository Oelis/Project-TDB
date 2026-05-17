using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public class DamageOverTimeEffect : EffectOverTime, ITickEffect
    {
        [SerializeField] private int damagePerTurn;
        public DamageType DamageType;
        
        public override int MaxStackSize => 99;
        public override bool CanBeStacked => true;
        public override EOTType EOTType => EOTType.Debuff;


        public void Tick()
        {
            Debug.Log($"[{GetType().Name}] has Tick");
            CurrentTarget.TakeDamage(damagePerTurn, DamageType,false,false);
        }
    }
}