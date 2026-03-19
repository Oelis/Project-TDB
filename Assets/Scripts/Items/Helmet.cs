using Enums;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu (fileName = "Helmet", menuName = "Equipment/Helmet")]
    public class Helmet : Armor
    {
        public override ArmorType armorType => ArmorType.Helmet;
    }
}