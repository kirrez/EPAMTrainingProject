using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour, IGame
{
    private IPlayer _player;
    private IGameHUD _gameHUD;
    private IGameCamera _gameCamera;
    private IGameSettings _gameSettings;

    private IPauseMenu _pauseMenu;
    private IVictoryMenu _victoryMenu;
    private IGameOverMenu _gameOverMenu;

    private bool _gamePaused = false;

    private Transform _startPosition;

    private void Awake()
    {
        _pauseMenu = new PauseMenu();
        _victoryMenu = new VictoryMenu();
        _gameOverMenu = new GameOverMenu();

        _player = ServiceLocator.GetPlayer();
        _gameHUD = ServiceLocator.GetGameHUD();
        _gameCamera = ServiceLocator.GetGameCamera();
        _gameSettings = ServiceLocator.GetGameSettings();
        var eventSystem = ServiceLocator.GetEventSystem();
        var playerSettings = ServiceLocator.GetPlayerSettings();

        _player.Killed += OnPlayerKilled;

        _victoryMenu.Proceeding += OnNextLevelClicked;

        _gameOverMenu.Restarting += RestartLevel;
        _gameOverMenu.Backing += BackToTitle;
        _gameOverMenu.Quitting += QuitGame;

        _pauseMenu.Resuming += ResumeGame;
        _pauseMenu.Restarting += RestartLevel;
        _pauseMenu.Backing += BackToTitle;
    }

    private void Start()
    {
        _gameHUD.Show();

        _pauseMenu.Hide();
        _victoryMenu.Hide();
        _gameOverMenu.Hide();
        Time.timeScale = 1f;
    }

    private void Update()
    {
        //Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused == true)
        {
            Time.timeScale = 1f;
            _pauseMenu.Hide();
            _gamePaused = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused == false)
        {
            Time.timeScale = 0f;
            _pauseMenu.Show();
            _gamePaused = true;
            return;
        }
    }

    private void RestartLevel()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }

    private void BackToTitle()
    {
        SceneManager.LoadScene(Scenes.Menu.ToString());
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        _pauseMenu.Hide();
        _gamePaused = false;
    }


    private void OnPlayerKilled()
    {
        Lose();
    }

    private void OnNextLevelClicked()
    {
        var firstLevel = (Levels)0;
        var currentLevel = (int)_gameSettings.CurrentLevel;
        var nextLevel = (int)_gameSettings.CurrentLevel + 1;
        var lastLevel = Enum.GetValues(typeof(Levels)).Length - 1;

        if (currentLevel == lastLevel)
        {
            _gameSettings.CurrentLevel = firstLevel;
            SceneManager.LoadScene(Scenes.Menu.ToString());
        }
        else
        {
            _gameSettings.CurrentLevel = (Levels)nextLevel;
            SceneManager.LoadScene(_gameSettings.CurrentLevel.ToString());
        }
    }

    public void Win()
    {
        _victoryMenu.Show();
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        _gameOverMenu.Show();
        var unitRepository = ServiceLocator.GetUnitRepository();
        unitRepository.StopChasingPlayer();
    }
}