using Items;
using Units.Configs;

namespace Units.Brain
{
    public class PlayerBrain : UnitBrain<PlayerBrain, PlayerConfig>
    {
        public void EquipGear()
        {
            if (!config) return;
            EquipArmor(config.Helmet,config.Chest,config.Legs);
            EquipWeapon(this.config.Weapon);
        }
        
        private void EquipArmor(params Armor[] item)
        {
            foreach (var armor in item)
            {
                if (!armor) continue;
                foreach (var passiveAbility in armor.PassiveAbilities)
                {
                    AbilityController.AddPassiveAbility(passiveAbility);
                }
            }  
        }

        private void EquipWeapon(Weapon item)
        {
            // if no weapon => add base active ability fist fight
            
            if (!item) return;
            foreach (var passiveAbility in item.PassiveAbilities)
            {
                AbilityController.AddPassiveAbility(passiveAbility);
            }
            AbilityController.AddActiveAbility(item.activeAbility);
        }

        public override PlayerBrain Build()
        {
            SetupAbilities();
            EquipGear();
            return this;
        }
        
        public override void Die()
        {
            Registry<PlayerBrain>.Remove(this);
            base.Die();
        }
    }
}