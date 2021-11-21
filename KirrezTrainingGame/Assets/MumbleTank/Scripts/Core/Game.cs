using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform StartPosition;

    private IGameCamera _gameCamera;
    private IPlayerHUD _playerHUD;
    private IGameUI _gameUI;
    private IPlayer _player;

    private void Awake()
    {
        //_pauseMenu = new PauseMenu();
        //_gameOverMenu = new GameOverMenu()

        _player = ServiceLocator.GetPlayer();
        _gameUI = ServiceLocator.GatGameUI();
        _playerHUD = ServiceLocator.GetPlayerHUD();
        _gameCamera = ServiceLocator.GetGameCamera();
        var playerSettings = ServiceLocator.GetPlayerSettings();

        var hitPoints = playerSettings.GetMaxHitpoints();

        _playerHUD.SetMaxHP(hitPoints);

        _player.HealthChanged += OnPlayerHealthChanged;
        _player.Killed += OnPlayerKilled;

        _player.SetStartPosition(StartPosition.position);
    }

    private void OnPlayerHealthChanged(int value)
    {
        _playerHUD.UpdateCurrentHP(value);
    }

    private void OnPlayerKilled()
    {
        _gameUI.ShowGameOverScreen();
    }

    private void Start()
    {
        _gameCamera.SetTarget(_player.GetTransform());
    }
}