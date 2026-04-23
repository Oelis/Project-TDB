using System;
using Enums;
using Interfaces;
using Sirenix.OdinInspector;
using Stats;
using Units;
using UnityEngine;

namespace Abilities.Effects
{
    public abstract class StatModifierEffect : EffectOverTime
    {
        public override bool CanBeStacked => true;
        
        protected abstract StatType StatType { get; }
        
        private bool IsValidValue(int value) => value != 0;
        
        [SerializeField,ValidateInput(nameof(IsValidValue), "The value of the modifier cannot be 0.")]
        protected int value;
        
        public void Handle(Query query)
        {
            if (query.StatType == StatType)
                query.Value = Mathf.Clamp(Operation(query), StatRanges.Get(StatType).Min, StatRanges.Get(StatType).Max);
        }

        public abstract int Operation(Query query);
    }
}
