using System;
using UnityEngine;

public class Game : MonoBehaviour, IGame
{
    //public Transform StartPosition;

    private IPlayer _player;

    private IPauseMenu _pauseMenu;
    private IGameCamera _gameCamera;
    private IVictoryMenu _victoryMenu;
    private IGameOverMenu _gameOverMenu;

    private bool _gamePaused = false;

    private Transform _startPosition;

    private void Awake()
    {
        var gameHUD = new GameHUD();
        _pauseMenu = new PauseMenu();
        _victoryMenu = new VictoryMenu();
        _gameOverMenu = new GameOverMenu();

        _player = ServiceLocator.GetPlayer();
        _gameCamera = ServiceLocator.GetGameCamera();
        var lightContainer = ServiceLocator.GetLight();
        var eventSystem = ServiceLocator.GetEventSystem();
        var playerSettings = ServiceLocator.GetPlayerSettings();

        _player.Killed += OnPlayerKilled;

    }

    private void OnPlayerKilled()
    {
        _gameOverMenu.Show();
    }

    private void Start()
    {
        _pauseMenu.Hide();
        _victoryMenu.Hide();
        _gameOverMenu.Hide();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused == true)
        {
            _pauseMenu.Hide();
            _gamePaused = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused == false)
        {
            _pauseMenu.Show();
            _gamePaused = true;
            return;
        }

        //Debug
        if (Input.GetKeyDown(KeyCode.V))
        {
            _victoryMenu.Show();
        }
    }
}