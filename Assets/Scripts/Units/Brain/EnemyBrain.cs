using System;
using Abilities;
using Interfaces;
using Stats;

namespace Units.Logic
{
    [Serializable]
    public class EnemyBrain : UnitBrain
    {
        private EnemyConfig _config;

        public EnemyBrain Build()
        {
            SetupAbilities();
            return this;
        }

        public EnemyBrain WithConfig(EnemyConfig config)
        {
            _config = config;
            return this;
        }
        
        public EnemyBrain WithStats(Stats.Stats stats)
        {
            this.Stats = new Stats.Stats(new StatsMediator(), this._config);
            return this;
        }
        
        public EnemyBrain WithSource(Unit source)
        {
            this.source = source;
            return this;
        }

        public EnemyBrain WithAbilityManager()
        {
            this.abilityManager = new AbilityManager(this);
            return this;
        }
        
        public void SetupAbilities()
        {
            if (!_config || abilityManager == null) return;

            foreach (var passiveAbility in _config.passiveAbilities)
            {
                abilityManager.AddPassiveAbility(passiveAbility);
            }
            foreach (var activeAbility in _config.activeAbilities)
            {
                abilityManager.AddActiveAbility(activeAbility);
            }
        }
    }
}