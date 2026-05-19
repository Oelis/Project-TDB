using System;
using Enums;
using Interfaces;
using UnityEngine;

namespace Abilities.Effects
{
    public abstract class BuffStatModifierEffect : StatModifierEffect, IFlippable
    {
        public override EOTType EOTType => EOTType.Buff;

        [SerializeField] private PositiveOperation operationType;

        protected override int Operation(Query query) => operationType switch
        {
            PositiveOperation.Add      => query.Value + value,
            PositiveOperation.Multiply => Mathf.RoundToInt(query.Value * (value / 100f)),
            _                          => query.Value
        };

        public void InitializeFlip(int val, PositiveOperation op, int duration)
        {
            value = val;
            operationType = op;
            _turnDuration = duration;
        }

        public EffectOverTime Flip()
        {
            DebuffStatModifierEffect flippedEffect = (DebuffStatModifierEffect)Activator.CreateInstance(Helpers.FlipDic[GetType()]);
            NegativeOperation flippedOperation = operationType == PositiveOperation.Add ? NegativeOperation.Subtract : NegativeOperation.Divide;
            flippedEffect.InitializeFlip(value, flippedOperation, _turnDuration);
            return flippedEffect;
        }
    }
}
