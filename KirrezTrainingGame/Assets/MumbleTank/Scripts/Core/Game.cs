using UnityEngine;

public class Game : MonoBehaviour
{
    public Transform StartPosition;

    private IPlayer _player;
    private IPauseMenu _pauseMenu;
    private IGameCamera _gameCamera;
    private IVictoryMenu _victoryMenu;
    private IGameOverMenu _gameOverMenu;

    private bool _gamePaused = false;

    private void Awake()
    {
        var gameHUD = new GameHUD();
        _pauseMenu = new PauseMenu();
        _victoryMenu = new VictoryMenu();
        _gameOverMenu = new GameOverMenu();

        _player = ServiceLocator.GetPlayer();
        _gameCamera = ServiceLocator.GetGameCamera();
        var playerSettings = ServiceLocator.GetPlayerSettings();

        _player.Killed += OnPlayerKilled;

        _player.SetStartPosition(StartPosition.position);
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