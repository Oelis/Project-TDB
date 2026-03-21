using System.Collections.Generic;
using Abilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Units
{
    public abstract class UnitConfig : ScriptableObject
    {
        public int attack = 10;
        public int defense = 10;
    
        public int intelligence = 10;
        public int dexterity = 10;
        public int strength = 10;
        public int constitution = 10;
        public int speed = 10;

        public float criticalChance = 0.15f;
        public float criticalDamageMultiplier = 1.5f;
        public float evadeRate = 0.15f;
        public float blockRate = 0.15f;

        public int physicalResist = 10;
        public int fireResist = 10;
        public int iceResist = 10;
        public int poisonResist = 10;
        public int lightningResist = 10;
    
        [TableList]
        public List<ActiveAbility> activeAbilities;
        [TableList]
        public List<PassiveAbility> passiveAbilities;
    }
}
