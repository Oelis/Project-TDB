using Units;
using Units.Brain;
using Units.Template;
using UnityEngine;

public class Test : MonoBehaviour
{
    public EncountersTemplate encounters;

    public Spawner spawner;
    
    
    public PlayerTemplate PlayerTemplate;
    
    public PlayerSquad playerSquad;

    private void Start()
    {
        spawner.SetEncounters(encounters);
        spawner.SpawnEnemies();
        
        playerSquad.AddPlayer(PlayerTemplate);
        spawner.SetPlayerSquad(playerSquad);
        spawner.SpawnPlayers();

        var player = Registry<PlayerBrain>.GetFirst();
        var enemy = Registry<EnemyBrain>.GetFirst();
        
        var ability = player.GetAbilityController().GetAbility(0);
        Debug.Log(ability.name);

        
        //Debug.Log(enemy);
        //player.GetAbilityController().CastAbility(player.GetAbilityController().GetAbility(0),enemy);

    }
}