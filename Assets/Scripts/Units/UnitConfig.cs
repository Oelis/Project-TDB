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

        public float criticalChance = 0.15f;
        public float criticalDamageMultiplier = 1.5f;
        public float evadeRate = 0.15f;
        public float blockRate = 0.15f;

        public int physicalResist;
        public int fireResist;
        public int iceResist;
        public int poisonResist;
        public int lightningResist;
    
        public List<ActiveAbility> activeAbilities;
        public List<PassiveAbility> passiveAbilities;
    }
}
