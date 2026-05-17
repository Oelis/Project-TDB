using Enums;
using UnityEngine;

namespace Abilities.Effects
{
    public abstract class DebuffStatModifierEffect : StatModifierEffect
    {
        public override EOTType EOTType => EOTType.Debuff;

        [SerializeField] private NegativeOperation operationType;

        protected override int Operation(Query query) => operationType switch
        {
            NegativeOperation.Subtract => query.Value - value,
            NegativeOperation.Divide   => Mathf.RoundToInt(query.Value / (float)value),
            _                          => query.Value
        };
    }
}
