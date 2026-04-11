using System.Collections.Generic;
using Abilities.Commands;
using Units;
using UnityEngine;

namespace Abilities
{
    public class AbilityController
    {
        private readonly List<ActiveAbility> _activeAbilities = new();
        private readonly List<PassiveAbility> _passiveAbilities = new();

        private readonly UnitBrain source;

        public AbilityController(UnitBrain source)
        {
            this.source = source;
            Debug.Log($"Ability Controller Created for {source.GetType().Name}");
        }

        public void CastAbility(ActiveAbility ability)
        {
            var builder = new AbilityCommandBuilder(source);
            foreach (var entry in ability.effectEntries)
                builder.AddEntry(entry);
            CastAbility(builder.Build());
        }

        public void CastAbility(List<AbilityCommand> commands)
        {
            foreach (var command in commands)
                command.Execute();
        }

        public void AddPassiveAbility(PassiveAbility ability)
        {
            _passiveAbilities.Add(ability);
            Debug.Log($"Passive Ability Added : {ability.name} on {source.GetType().Name}");
        }

        public void AddActiveAbility(ActiveAbility ability)
        {
            _activeAbilities.Add(ability);
            Debug.Log($"Active Ability Added : {ability.name} on {source.GetType().Name}");
        }

        public ActiveAbility GetAbility(int index) => _activeAbilities[index];
    }
}
