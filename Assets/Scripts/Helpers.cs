using System.Collections.Generic;
using Enums;
using UnityEngine;

public static class Helpers
{
    public static Dictionary<DamageType, StatType> ResistDic = new Dictionary<DamageType, StatType>()
    {
        { DamageType.PhysicalDamage,   StatType.PhysicalResist   },
        { DamageType.BleedDamage,      StatType.BleedResist      },
        { DamageType.FireDamage,       StatType.FireResist       },
        { DamageType.IceDamage,        StatType.IceResist        },
        { DamageType.LightningDamage,  StatType.LightningResist  },
        { DamageType.PoisonDamage,     StatType.PoisonResist     },
    };
}
