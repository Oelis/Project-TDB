using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Abilities.Effects
{
    public abstract class DebuffStatModifierEffect : StatModifierEffect, IFlippable
    {
        public override EOTType EOTType => EOTType.Debuff;

        [SerializeField] private NegativeOperation operationType;

        protected override int Operation(Query query) => operationType switch
        {
            NegativeOperation.Subtract => query.Value - value,
            NegativeOperation.Divide   => Mathf.RoundToInt(query.Value / (float)value),
            _                          => query.Value
        };

        public void InitializeFlip(int val, NegativeOperation op, int duration)
        {
            value = val;
            operationType = op;
            _turnDuration = duration;
        }

        public EffectOverTime Flip()
        {
            BuffStatModifierEffect flippedEffect = (BuffStatModifierEffect)Activator.CreateInstance(Helpers.FlipDic[GetType()]); 
            PositiveOperation flippedOperation = operationType == NegativeOperation.Subtract ? PositiveOperation.Add : PositiveOperation.Multiply;
            flippedEffect.InitializeFlip(value, flippedOperation, _turnDuration);
            return flippedEffect;
        }
    }
}
