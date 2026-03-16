using UnityEngine;

public class Stats
{
    private readonly StatsMediator mediator;
    readonly UnitBaseStats baseStats;
    
    public StatsMediator Mediator => mediator;
    
    public int Intelligence
    {
        get
        {
            var q = new Query(StatType.Intelligence, baseStats.intelligence);
            mediator.PerformQuery(this,q);
            return q.Value;
        }
    }

    public int Strength
    {
        get
        {
            var q = new Query(StatType.Strength, baseStats.strength);
            mediator.PerformQuery(this,q);
            return q.Value;
        }
    }

    public Stats(StatsMediator mediator, UnitBaseStats baseStats)
    {
        this.mediator = mediator;
        this.baseStats = baseStats;
    }
}
