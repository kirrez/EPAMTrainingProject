using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject LevelMapPrefab;

    public List<EnemySpawner> EnemySpawners;

    private IGameCamera _gameCamera;
    private IPlayerHUD _playerHUD;
    private IOverlayCanvas _overlayCanvas;
    private IGameUI _gameUI;
    private IPlayer _player;

    private GameObject _levelMap;
    private Transform _playerStartPosition;

    private void Awake()
    {
        _player = ServiceLocator.GetPlayer();
        _gameUI = ServiceLocator.GatGameUI();
        _playerHUD = ServiceLocator.GetPlayerHUD();
        _gameCamera = ServiceLocator.GetGameCamera();
        _overlayCanvas = ServiceLocator.GetOverlayCanvas();

        foreach (var spawner in EnemySpawners)
        {
            spawner.EnemySpawned += OnEnemySpawned;
        }

        var hitPoints = PlayerSettings.UserMaxHitpoints;
        _playerHUD.SetMaxHP(hitPoints);

        _player.HealthChanged += OnPlayerHealthChanged;
        _player.Killed += OnPlayerKilled;

        _levelMap = Instantiate(LevelMapPrefab);
        // taking start position from LevelMap's prefab
        var startPosition = LevelMapPrefab.GetComponentInChildren<StartPosition>();
        _playerStartPosition = startPosition.gameObject.transform;
        _player.SetStartPosition(_playerStartPosition);
    }

    private void OnPlayerHealthChanged(int value)
    {
        _playerHUD.UpdateCurrentHP(value);
    }

    private void OnPlayerKilled()
    {
        _gameUI.ShowGameOverScreen();
        _player.Killed -= OnPlayerKilled;
    }

    private void Start()
    {
        AttachCameraToPlayer();
    }

    private void OnEnemySpawned(IEnemy enemy)
    {
        enemy.SetTarget(_player.GetTransform());
        enemy.GetCanvas(_overlayCanvas);
    }

    private void AttachCameraToPlayer()
    {
        _gameCamera.SetTarget(_player.GetTransform());
    }
}
