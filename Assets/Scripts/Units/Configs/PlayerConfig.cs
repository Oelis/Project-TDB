using Items;
using UnityEngine;

namespace Units
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Unit/PlayerConfig")]
    public class PlayerConfig : UnitConfig
    {
        public Helmet Helmet;
        public Chest Chest;
        public Legs Legs;
        public Weapon Weapon;
    }
}