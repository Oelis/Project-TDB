using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public abstract class DamageOverTimeEffect :EffectOverTime, IDebuff
    {
        public int damagePerTurn;
        
        public abstract DamageType DamageType { get; }
        
        public override void Tick()
        {
            Debug.Log($"[{GetType().Name}] has Tick");
            CurrentTarget.TakeDamage(damagePerTurn, DamageType);
        }
        
        
    }
}