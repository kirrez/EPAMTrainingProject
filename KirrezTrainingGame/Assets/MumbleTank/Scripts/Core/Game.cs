using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Player Player;
    public List<EnemySpawner> EnemySpawners;

    private PlayerHUD _playerHUD;
    private Canvas _enemyCanvas;
    private IGameCamera _gameCamera;

    private void Awake()
    {
        _playerHUD = FindObjectOfType<PlayerHUD>();
        _gameCamera = ServiceLocator.GetGameCamera();

        var enemyCanvasPrefab = Resources.Load<GameObject>("Prefabs/UI/EnemyCanvas");
        _enemyCanvas = Instantiate(enemyCanvasPrefab.GetComponent<Canvas>());

        foreach (var spawner in EnemySpawners)
        {
            spawner.EnemySpawned += OnEnemySpawned;
        }

        var hitPoints = PlayerSettings.UserMaxHitpoints;
        _playerHUD.SetMaxHP(hitPoints);

        Player.HealthChanged += OnPlayerHealthChanged;
    }

    private void OnPlayerHealthChanged(int value)
    {
        _playerHUD.UpdateCurrentHP(value);
    }

    private void Start()
    {
        AttachCameraToPlayer();
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.SetTarget(Player.transform);
        enemy.EnemyCanvas = _enemyCanvas;
    }

    private void AttachCameraToPlayer()
    {
        _gameCamera.SetTarget(Player.transform);
    }
}
