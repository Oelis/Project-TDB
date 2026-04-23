using System.Collections.Generic;
using Enums;

namespace Stats
{
    public readonly struct StatRange
    {
        public readonly StatType StatType;
        public readonly int Min;
        public readonly int Max;

        public StatRange(StatType statType, int min, int max)
        {
            StatType = statType;
            Min      = min;
            Max      = max;
        }
    }

    public static class StatRanges
    {
        public static readonly IReadOnlyDictionary<StatType, StatRange> Stats = new Dictionary<StatType, StatRange>
        {
            // Primary stats
            { StatType.Attack,                   new StatRange(StatType.Attack,                   0,   100) },
            { StatType.Defense,                  new StatRange(StatType.Defense,                  0,   100) },
            { StatType.Strength,                 new StatRange(StatType.Strength,                 0,   100) },
            { StatType.Intelligence,             new StatRange(StatType.Intelligence,             0,   100) },
            { StatType.Dexterity,                new StatRange(StatType.Dexterity,                0,   100) },
            { StatType.Constitution,             new StatRange(StatType.Constitution,             0,   100) },
            { StatType.Speed,                    new StatRange(StatType.Speed,                    0,   100) },

            // Combat stats
            { StatType.CriticalChance,           new StatRange(StatType.CriticalChance,           0,   100) },
            { StatType.CriticalDamageMultiplier, new StatRange(StatType.CriticalDamageMultiplier, 100, 500) },
            { StatType.EvadeRate,                new StatRange(StatType.EvadeRate,                0,   100) },
            { StatType.BlockRate,                new StatRange(StatType.BlockRate,                0,   100) },

            // Resistances
            { StatType.PhysicalResist,           new StatRange(StatType.PhysicalResist,           -100,   100) },
            { StatType.FireResist,               new StatRange(StatType.FireResist,               -100,   100) },
            { StatType.IceResist,                new StatRange(StatType.IceResist,                -100,   100) },
            { StatType.PoisonResist,             new StatRange(StatType.PoisonResist,             -100,   100) },
            { StatType.LightningResist,          new StatRange(StatType.LightningResist,          -100,   100) },
            { StatType.BleedResist,              new StatRange(StatType.BleedResist,              -100,   100) },
        };

        public static StatRange Get(StatType statType) => Stats[statType];
    }
}
