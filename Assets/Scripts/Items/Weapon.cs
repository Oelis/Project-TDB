using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Weapon", menuName = "Equipment/Weapon")]
public class Weapon : EquippableItem
{
    private ItemType itemType = ItemType.Weapon;
    public AbilityData activeAbility;
    public WeaponType WeaponType;
    public HoldType HoldType;
    public int criticalChance;
    public int criticalDamageMultiplier;
    
}

public enum WeaponType
{
    Staff,
    Sword,
    Dagger,
    Shield,
    Axe,
    Bow
}

public enum HoldType
{
    MainHand,
    OffHand,
    TwoHand,
    EitherHand
}