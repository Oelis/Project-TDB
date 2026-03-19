using Enums;
using UnityEngine;

namespace Items
{
    public abstract class Armor : EquippableItem
    {
        private ItemType itemType = ItemType.Armor;
        public abstract ArmorType armorType { get; }
        public float evadeRate;
        public float blockRate;

        public int physicalResist;
        public int fireResist;
        public int iceResist;
        public int poisonResist;
        public int lightningResist;
    }
}