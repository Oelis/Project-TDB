using Enums;
using UnityEngine;

namespace Abilities.Effects
{
    public abstract class BuffStatModifierEffect : StatModifierEffect
    {
        public override EOTType EOTType => EOTType.Buff;

        [SerializeField] private PositiveOperation operationType;

        protected override int Operation(Query query) => operationType switch
        {
            PositiveOperation.Add      => query.Value + value,
            PositiveOperation.Multiply => Mathf.RoundToInt(query.Value * (value / 100f)),
            _                          => query.Value
        };
    }
}
