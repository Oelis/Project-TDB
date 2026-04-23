using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public abstract class DamageOverTimeEffect : EffectOverTime, IDebuff, ITickEffect
    {
        public int damagePerTurn;
        public override int MaxStackSize => 99;
        public abstract StatType ResistanceStat { get; }

        public void Tick()
        {
            Debug.Log($"[{GetType().Name}] has Tick");
            CurrentTarget.TakeDamage(damagePerTurn, GetType(), ResistanceStat);
        }
    }
}