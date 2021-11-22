using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform StartPosition;

    private IGameCamera _gameCamera;
    private IPlayerHUD _playerHUD; // 2nd iteration, must be refactored
    private IPlayer _player;
    //private IGameUI _gameUI; // 2nd iteration (out of date)

    //3rd iteration, up to date
    private IPauseMenu _pauseMenu;
    private IVictoryMenu _victoryMenu;
    private IGameOverMenu _gameOverMenu;

    private bool _gamePaused = false;

    private void Awake()
    {
        _pauseMenu = new PauseMenu();
        _victoryMenu = new VictoryMenu();
        _gameOverMenu = new GameOverMenu();

        _player = ServiceLocator.GetPlayer();
        _playerHUD = ServiceLocator.GetPlayerHUD(); // !
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
        //_gameUI.ShowGameOverScreen();
        _gameOverMenu.Show();
    }

    private void Start()
    {
        _gameCamera.SetTarget(_player.GetTransform());
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