using Enums;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu (fileName = "Legs", menuName = "Equipment/Legs")]
    public class Legs : Armor
    {
        public override ArmorType armorType => ArmorType.Legs;
    }
}