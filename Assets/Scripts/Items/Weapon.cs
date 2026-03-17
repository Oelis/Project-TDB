using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Weapon", menuName = "Equipment/Weapon")]
public class Weapon : EquippableItem
{
    private ItemType itemType = ItemType.Weapon;
    public Ability activeAbility;
    public WeaponType WeaponType;
    public HoldType HoldType;
    public int criticalChance;
    public int criticalDamageMultiplier;
    
}