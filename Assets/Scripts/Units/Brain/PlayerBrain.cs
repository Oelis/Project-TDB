using Abilities;
using Items;
using JetBrains.Annotations;
using Stats;

namespace Units.Logic
{
    public class PlayerBrain : UnitBrain
    {
        private PlayerConfig _config;
        
        public PlayerBrain WithSource(Unit source)
        {
            this.source = source;
            return this;
        }
        
        public PlayerBrain WithAbilityManager()
        {
            this.abilityManager = new AbilityManager(this);
            return this;
        }
        
        public PlayerBrain WithPlayerConfig(PlayerConfig config)
        {
            this._config = config;
            return this;
        }

        public PlayerBrain WithStats(Stats.Stats stats)
        {
            this.Stats = new Stats.Stats(new StatsMediator(), this._config);
            return this;
        }
        
        public void EquipGear()
        {
            if (!_config) return;
            EquipArmor(this._config.Helmet,this._config.Chest,this._config.Legs);
            EquipWeapon(this._config.Weapon);
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

        public PlayerBrain Build()
        {
            SetupAbilities();
            EquipGear();
            return this;
        }

        private void EquipArmor(params Armor[] item)
        {
            foreach (var armor in item)
            {
                if (!armor) continue;
                foreach (var passiveAbility in armor.PassiveAbilities)
                {
                    abilityManager.AddPassiveAbility(passiveAbility);
                }
            }  
        }

        private void EquipWeapon(Weapon item)
        {
            // if no weapon => add base active ability fist fight
            
            if (!item) return;
            foreach (var passiveAbility in item.PassiveAbilities)
            {
                abilityManager.AddPassiveAbility(passiveAbility);
            }
            abilityManager.AddActiveAbility(item.activeAbility);
        }
    }
}