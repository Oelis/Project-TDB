using Enums;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu (fileName = "Chest", menuName = "Equipment/Chest")]
    public class Chest : Armor
    {
        public override ArmorType armorType => ArmorType.Chest;
    }
}