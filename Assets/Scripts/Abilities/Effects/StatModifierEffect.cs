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
        
        private readonly StatType _type;
        [SerializeField] private int value;
        
        
        public void Handle(Query query)
        {
            if (query.StatType == _type)
            {
                query.Value = Operation(query);
            }
        }
        public abstract int Operation(Query query);


    }
}