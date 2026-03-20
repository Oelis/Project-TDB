using Sirenix.OdinInspector;
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
        [Required] public Texture icon;
        [Required] public string name;
        [Required] public string description;
        public int stackSize;
        public int rarity;
    
    }
}