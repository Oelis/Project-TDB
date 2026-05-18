using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Abilities.Effects
{
    [Serializable]
    public class HealOverTime : EffectOverTime, ITickEffect
    {
        public override int MaxStackSize => 99;
        public override bool CanBeStacked => true;
        public override EOTType EOTType => EOTType.Buff;
        
        [SerializeField] private int healPerTurn;
        public void Tick()
        {
            Debug.Log($"[{GetType().Name}] has Tick");
            CurrentTarget.Heal(healPerTurn);
        }
    }
}