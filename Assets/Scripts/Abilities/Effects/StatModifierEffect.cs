using System;
using Enums;
using Interfaces;
using Stats;
using Units;
using UnityEngine;

namespace Abilities.Effects
{
    public abstract class StatModifierEffect : EffectOverTime
    {
        public override bool CanBeStacked => true;

        protected abstract StatType StatType { get ; }
        [SerializeField] protected int value;
        
        
        public void Handle(Query query)
        {
            if (query.StatType == StatType)
            {
                query.Value = Operation(query);
            }
        }
        public abstract int Operation(Query query);


    }
}