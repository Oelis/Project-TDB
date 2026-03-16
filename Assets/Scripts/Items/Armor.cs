using UnityEngine;

[CreateAssetMenu (fileName = "Armor", menuName = "Equipment/Armor")]
public class Armor : EquippableItem
{
    private ItemType itemType = ItemType.Armor;
    public ArmorType armorType;
    public int evadeRate;
    public int blockRate;

    public int physicalResist;
    public int fireResist;
    public int iceResist;
    public int poisonResist;
    public int lightningResist;
}

public enum ArmorType
{
    Helmet,
    Chest,
    Legs
}

