using System;
using System.Collections.Generic;
using Abilities.Effects;
using Abilities.Effects.Buffs.Immunity;
using Abilities.Effects.Debuffs;
using Abilities.Effects.StatModif;
using Enums;

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
    
    public static Dictionary<Type, Type> FlipDic = new Dictionary<Type, Type>()
    {
        { typeof(AttackUp),                      typeof(AttackDown)                      },
        { typeof(AttackDown),                    typeof(AttackUp)                        },
        { typeof(DefenseUp),                     typeof(DefenseDown)                     },
        { typeof(DefenseDown),                   typeof(DefenseUp)                       },
        { typeof(IntelligenceUp),                typeof(IntelligenceDown)                },
        { typeof(IntelligenceDown),              typeof(IntelligenceUp)                  },
        { typeof(DexterityUp),                   typeof(DexterityDown)                   },
        { typeof(DexterityDown),                 typeof(DexterityUp)                     },
        { typeof(StrengthUp),                    typeof(StrengthDown)                    },
        { typeof(StrengthDown),                  typeof(StrengthUp)                      },
        { typeof(ConstitutionUp),                typeof(ConstitutionDown)                },
        { typeof(ConstitutionDown),              typeof(ConstitutionUp)                  },
        { typeof(SpeedUp),                       typeof(SpeedDown)                       },
        { typeof(SpeedDown),                     typeof(SpeedUp)                         },
        { typeof(CriticalChanceUp),              typeof(CriticalChanceDown)              },
        { typeof(CriticalChanceDown),            typeof(CriticalChanceUp)                },
        { typeof(CriticalDamageMultiplierUp),    typeof(CriticalDamageMultiplierDown)    },
        { typeof(CriticalDamageMultiplierDown),  typeof(CriticalDamageMultiplierUp)      },
        { typeof(EvadeRateUp),                   typeof(EvadeRateDown)                   },
        { typeof(EvadeRateDown),                 typeof(EvadeRateUp)                     },
        { typeof(BlockRateUp),                   typeof(BlockRateDown)                   },
        { typeof(BlockRateDown),                 typeof(BlockRateUp)                     },
        { typeof(FireResistUp),                  typeof(FireResistDown)                  },
        { typeof(FireResistDown),                typeof(FireResistUp)                    },
        { typeof(IceResistUp),                   typeof(IceResistDown)                   },
        { typeof(IceResistDown),                 typeof(IceResistUp)                     },
        { typeof(PoisonResistUp),                typeof(PoisonResistDown)                },
        { typeof(PoisonResistDown),              typeof(PoisonResistUp)                  },
        { typeof(LightningResistUp),             typeof(LightningResistDown)             },
        { typeof(LightningResistDown),           typeof(LightningResistUp)               },
        { typeof(BleedResistUp),                 typeof(BleedResistDown)                 },
        { typeof(BleedResistDown),               typeof(BleedResistUp)                   },
        { typeof(PhysicalResistUp),              typeof(PhysicalResistDown)              },
        { typeof(PhysicalResistDown),            typeof(PhysicalResistUp)                },
        { typeof(DebuffImmunity),                typeof(BuffImmunity)                    },
        { typeof(BuffImmunity),                  typeof(DebuffImmunity)                  },
        { typeof(DamageOverTimeEffect),          typeof(HealOverTime)                    },
        { typeof(HealOverTime),                  typeof(DamageOverTimeEffect)            }
    };
}
