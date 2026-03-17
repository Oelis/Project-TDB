using TMPro.EditorUtilities;
using UnityEngine;

public class Stats
{
    private readonly StatsMediator mediator;
    readonly UnitConfig config;
    
    public StatsMediator Mediator => mediator;
    
    public int Intelligence
    {
        get
        {
            var q = new Query(StatType.Intelligence, config.intelligence);
            mediator.PerformQuery(this,q);
            return q.Value;
        }
    }

    public int Strength
    {
        get
        {
            var q = new Query(StatType.Strength, config.strength);
            mediator.PerformQuery(this,q);
            return q.Value;
        }
    }

    public float CriChance
    {
        get
        {
            var q = new Query(StatType.CriticalChance, config.criticalChance);
            mediator.PerformQuery(this,q);
            return q.Value;
        }
    }

    public Stats(StatsMediator mediator, UnitConfig config)
    {
        this.mediator = mediator;
        this.config = config;
    }
}
