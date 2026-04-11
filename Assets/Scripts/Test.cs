using System.Collections.Generic;
using Sirenix.OdinInspector;
using Units;
using Units.Brain;
using Units.Template;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Test : MonoBehaviour
{
    public EncountersTemplate encounters;

    [Required] public Spawner spawner;
    
    [Required] public PlayerTemplate PlayerTemplate;
    
    [Required] public PlayerSquad playerSquad;
    

    private void Start()
    {
        spawner.SpawnEnemies(encounters);
        
        playerSquad.AddPlayer(PlayerTemplate,4);
        spawner.SpawnPlayers(playerSquad);

        var player = Registry<PlayerBrain>.GetFirst();
        var enemy = Registry<EnemyBrain>.GetFirst();
        
        var ability = player.GetAbilityController().GetAbility(0);
        
        player.StartTurn();
        enemy.StartTurn();
        player.GetAbilityController().CastActiveAbility(ability);
        
    }

    private void Update()
    {
        if(Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            var enemy = Registry<EnemyBrain>.GetFirst();
            var player = Registry<PlayerBrain>.GetFirst();
            player.StartTurn();
            enemy.StartTurn();
            
            player.EndTurn();
            enemy.EndTurn();
        }
    }
}