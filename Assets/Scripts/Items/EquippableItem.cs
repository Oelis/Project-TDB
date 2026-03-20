using System.Collections.Generic;
using Abilities;
using Interfaces;
using Sirenix.OdinInspector;

namespace Items
{
    public abstract class EquippableItem : Item
    {
        public int intelligence = 10;
        public int dexterity = 10;
        public int force = 10;
        public int constitution = 10;
        public int speed = 10;
    
        public List<PassiveAbility> PassiveAbilities;
    }
}






