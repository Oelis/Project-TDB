using UnityEngine;

public class Stats
{
    private readonly StatsMediator mediator;
    readonly UnitConfig _config;
    
    public StatsMediator Mediator => mediator;
    
    public int Intelligence
    {
        get
        {
            var q = new Query(StatType.Intelligence, _config.intelligence);
            mediator.PerformQuery(this,q);
            return q.Value;
        }
    }

    public int Strength
    {
        get
        {
            var q = new Query(StatType.Strength, _config.strength);
            mediator.PerformQuery(this,q);
            return q.Value;
        }
    }

    public Stats(StatsMediator mediator, UnitConfig config)
    {
        this.mediator = mediator;
        this._config = config;
    }
}
