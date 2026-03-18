using Enums;
using Units;

namespace Stats
{
    public class Stats
    {
        private readonly StatsMediator _mediator;
        readonly UnitConfig _statConfig;
    
        public StatsMediator Mediator => _mediator;
    
        public int Intelligence
        {
            get
            {
                var q = new Query(StatType.Intelligence, _statConfig.intelligence);
                _mediator.PerformQuery(this,q);
                return q.Value;
            }
        }

        public int Strength
        {
            get
            {
                var q = new Query(StatType.Strength, _statConfig.strength);
                _mediator.PerformQuery(this,q);
                return q.Value;
            }
        }

        public float CriChance
        {
            get
            {
                var q = new Query(StatType.CriticalChance, _statConfig.criticalChance);
                _mediator.PerformQuery(this,q);
                return q.Value;
            }
        }

        public Stats(StatsMediator mediator, UnitConfig statConfig)
        {
            this._mediator = mediator;
            this._statConfig = statConfig;
        }
    }
}
