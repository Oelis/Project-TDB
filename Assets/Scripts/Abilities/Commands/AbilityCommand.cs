using Interfaces;
using Units;

namespace Abilities.Commands
{
    public class AbilityCommand
    {
        private readonly IEffect _effect;
        private readonly UnitBrain _source;
        private readonly UnitBrain _target;

        public AbilityCommand(IEffect effect, UnitBrain source, UnitBrain target)
        {
            _effect = effect;
            _source = source;
            _target = target;
        }

        public void Execute() => _effect.Apply(_source, _target);
    }
}