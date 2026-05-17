using Enums;

namespace Abilities.Effects.StatModif
{
    public class DexterityDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Dexterity;
    }
}
