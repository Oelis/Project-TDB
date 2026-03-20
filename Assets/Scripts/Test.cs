using Sirenix.OdinInspector;
using Units;
using Units.Brain;
using Units.Template;
using UnityEngine;
using Utils;

public class Test : MonoBehaviour
{
    public EncountersTemplate encounters;

    [Required] public Spawner spawner;
    
    [Required] public PlayerTemplate PlayerTemplate;
    
    [Required] public PlayerSquad playerSquad;

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
        
        player.GetAbilityController().CastAbility(ability,enemy);

    }
}