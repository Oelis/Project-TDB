using Enums;
using Units;

namespace Stats
{
    public class Stats
    {
        private readonly StatsModifLogic _modifLogic;
        readonly UnitConfig _statConfig;
        
        public Stats(StatsModifLogic modifLogic, UnitConfig statConfig)
        {
            this._modifLogic = modifLogic;
            this._statConfig = statConfig;
        }
        
        public int Attack
        {
            get
            {
                var q = new Query(StatType.Attack, _statConfig.attack);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int Defense
        {
            get
            {
                var q = new Query(StatType.Defense, _statConfig.defense);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int Intelligence
        {
            get
            {
                var q = new Query(StatType.Intelligence, _statConfig.intelligence);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int Dexterity
        {
            get
            {
                var q = new Query(StatType.Dexterity, _statConfig.dexterity);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int Strength
        {
            get
            {
                var q = new Query(StatType.Strength, _statConfig.strength);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int Constitution
        {
            get
            {
                var q = new Query(StatType.Constitution, _statConfig.constitution);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int Speed
        {
            get
            {
                var q = new Query(StatType.Speed, _statConfig.speed);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }
        public int CriticalChance
        {
            get
            {
                var q = new Query(StatType.CriticalChance, _statConfig.criticalChance);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int CriticalDamageMultiplier
        {
            get
            {
                var q = new Query(StatType.CriticalDamageMultiplier,_statConfig.criticalDamageMultiplier);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int EvadeRate
        {
            get
            {
                var q = new Query(StatType.EvadeRate, _statConfig.evadeRate);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int BlockRate
        {
            get
            {
                var q = new Query(StatType.BlockRate, _statConfig.blockRate);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int FireResist
        {
            get
            {
                var q = new Query(StatType.FireResist, _statConfig.fireResist);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int IceResist
        {
            get
            {
                var q = new Query(StatType.IceResist, _statConfig.iceResist);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int PoisonResist
        {
            get
            {
                var q = new Query(StatType.PoisonResist, _statConfig.poisonResist);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int LightningResist
        {
            get
            {
                var q = new Query(StatType.LightningResist, _statConfig.lightningResist);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int PhysicalResist
        {
            get
            {
                var q = new Query(StatType.PhysicalResist, _statConfig.physicalResist);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        public int BleedResist
        {
            get
            {
                var q = new Query(StatType.BleedResist, _statConfig.bleedResist);
                _modifLogic.PerformQuery(q);
                return q.Value;
            }
        }

        
    }

   
}
