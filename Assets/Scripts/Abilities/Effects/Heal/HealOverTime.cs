using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public class HealOverTime : EffectOverTime, ITickEffect, IFlippable
    {
        public override int MaxStackSize => 99;
        public override bool CanBeStacked => true;
        public override EOTType EOTType => EOTType.Buff;

        [SerializeField] protected int healPerTurn;

        public void InitializeFlip(int heal, int duration)
        {
            healPerTurn = heal;
            _turnDuration = duration;
        }

        public EffectOverTime Flip()
        {
            DamageOverTimeEffect flippedEffect = new DamageOverTimeEffect();
            flippedEffect.InitializeFlip(healPerTurn, DamageType.BleedDamage, _turnDuration);
            return flippedEffect;
        }

        public void Tick()
        {
            Debug.Log($"[{GetType().Name}] has Tick");
            CurrentTarget.Heal(healPerTurn);
        }
    }
}