using UnityEngine;

namespace Items
{
    public enum ItemType
    {
        Armor,
        Weapon,
        Consummable
    }
    public abstract class Item : ScriptableObject
    {
        public Texture icon;
        public string name;
        public string description;
        public int stackSize;
        public int rarity;
    
    }
}