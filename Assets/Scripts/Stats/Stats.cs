using Enums;
using Units;

namespace Stats
{
    public class Stats
    {
        private readonly StatsMediator mediator;
        readonly UnitConfig _statConfig;
    
        public StatsMediator Mediator => mediator;
    
        public int Intelligence
        {
            get
            {
                var q = new Query(StatType.Intelligence, _statConfig.intelligence);
                mediator.PerformQuery(this,q);
                return q.Value;
            }
        }

        public int Strength
        {
            get
            {
                var q = new Query(StatType.Strength, _statConfig.strength);
                mediator.PerformQuery(this,q);
                return q.Value;
            }
        }

        public float CriChance
        {
            get
            {
                var q = new Query(StatType.CriticalChance, _statConfig.criticalChance);
                mediator.PerformQuery(this,q);
                return q.Value;
            }
        }

        public Stats(StatsMediator mediator, UnitConfig statConfig)
        {
            this.mediator = mediator;
            this._statConfig = statConfig;
        }
    }
}
