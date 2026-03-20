using System;
using Units.Configs;

namespace Units.Brain
{
    [Serializable]
    public class EnemyBrain : UnitBrain<EnemyBrain,EnemyConfig>
    {
        public override EnemyBrain Build()
        {
            SetupAbilities();
            return this;
        }

        public override void Die()
        {
            Registry<EnemyBrain>.Remove(this);
            base.Die();
        }
    }
}