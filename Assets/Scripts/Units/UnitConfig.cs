using System.Collections.Generic;
using Abilities;
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

        public int criticalChance;
        public int criticalDamageMultiplier;
        public int evadeRate;
        public int blockRate;

        public int physicalResist;
        public int fireResist;
        public int iceResist;
        public int poisonResist;
        public int lightningResist;
    
        public List<ActiveAbility> activeAbilities;
        public List<PassiveAbility> passiveAbilities;
    }
}
