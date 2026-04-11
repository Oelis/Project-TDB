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
            
        }

        public void CastActiveAbility(ActiveAbility ability)
        {
            var builder = new AbilityCommandBuilder(source);
            foreach (var entry in ability.effectEntries)
                builder.AddEntry(entry);
            ExecuteAbility(builder.Build());
        }

        public void CastPassiveAbilities()
        {
            Debug.Log($"{source.GetSource().name} cast passive abilities.");
            foreach (var ability in _passiveAbilities)
            {
                var builder = new AbilityCommandBuilder(source);
                foreach (var entry in ability.effectEntries)
                    builder.AddEntry(entry);
                ExecuteAbility(builder.Build());
            }
        }

        private void ExecuteAbility(List<AbilityCommand> commands)
        {
            foreach (var command in commands)
                command.Execute();
        }

        public void AddPassiveAbility(PassiveAbility ability)
        {
            _passiveAbilities.Add(ability);
            Debug.Log($"Passive Ability Added : {ability.name} on {source.GetSource().name}");
        }

        public void AddActiveAbility(ActiveAbility ability)
        {
            _activeAbilities.Add(ability);
            Debug.Log($"Active Ability Added : {ability.name} on {source.GetSource().name}");
        }

        public ActiveAbility GetAbility(int index) => _activeAbilities[index];
    }
}
