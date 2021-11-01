using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerShooting Player;
    public List<EnemySpawner> EnemySpawners;

    private void Awake()
    {
        foreach (var spawner in EnemySpawners)
        {
            spawner.EnemySpawned += OnEnemySpawned;
        }
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.SetTarget(Player.transform);
    }
}
