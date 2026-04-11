using System.Collections.Generic;
using Abilities.Targeting;
using Interfaces;
using Units;

namespace Abilities.Commands
{
    public class AbilityCommandBuilder
    {
        private readonly List<AbilityCommand> _commands = new();
        private readonly UnitBrain _source;

        public AbilityCommandBuilder(UnitBrain source)
        {
            _source = source;
        }

        public AbilityCommandBuilder AddEffect(IEffect effect, ITargetingStrategy targeting)
        {                                                                                                                                                                                                                                                    foreach (var target in targeting.Resolve())
                _commands.Add(new AbilityCommand(effect, _source, target));                                                                                                                                                                            
            return this;                                                                                                                                                                                                                               
        }

        public AbilityCommandBuilder AddEffect(IEffect effect, ICasterTargetingStrategy targeting)                                                                                                                                                     
        {
            foreach (var target in targeting.Resolve(_source))
                _commands.Add(new AbilityCommand(effect, _source, target));
            return this;
        }

        public AbilityCommandBuilder AddEntry(EffectEntry entry)
            => AddEffect(entry.effect, entry.targeting);

        public List<AbilityCommand> Build() => _commands;
    }
}
