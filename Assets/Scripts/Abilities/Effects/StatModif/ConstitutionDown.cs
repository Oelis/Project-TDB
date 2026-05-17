using Enums;

namespace Abilities.Effects.StatModif
{
    public class ConstitutionDown : DebuffStatModifierEffect
    {
        protected override StatType StatType => StatType.Constitution;
    }
}
