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

        public int criticalChance = 15;
        public int criticalDamageMultiplier = 150;
        public int evadeRate = 15;
        public int blockRate = 15;

        public int physicalResist = 10;
        public int fireResist = 10;
        public int iceResist = 10;
        public int poisonResist = 10;
        public int lightningResist = 10;
        public int bleedResist = 10;
    
        [TableList]
        public List<ActiveAbility> activeAbilities;
        [TableList]
        public List<PassiveAbility> passiveAbilities;
    }
}
