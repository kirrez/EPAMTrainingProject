using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    private MySceneManagment sceneManager;

    public bool GamePaused { get; set; } = false;

    public GameObject pauseScreen;
    public GameObject gameoverScreen;
    public GameObject victoryScreen;
    // text blinking effect
    public Image victoryFront;
    public Image victoryBack;
    //private float hue = 0f;
    private bool _isVictory = false;

    [Header("Pause Screen")]
    public Button Resume_PauseButton;
    public Button Restart_PauseButton;
    public Button BackToTitle_PauseButton;

    [Header("Game Over Screen")]
    public Button RestartGOButton;
    public Button BackToTitleGOButton;
    public Button QuitGOButton;

    [Header("Victory Screen")]
    public Button NextLevelButton;

    private void Awake()
    {
        sceneManager = GetComponent<MySceneManagment>();

        Resume_PauseButton.onClick.AddListener(OnResumeClick);
        Restart_PauseButton.onClick.AddListener(OnRestartClick);
        BackToTitle_PauseButton.onClick.AddListener(OnBackToTitleClick);

        RestartGOButton.onClick.AddListener(OnRestartClick);
        BackToTitleGOButton.onClick.AddListener(OnBackToTitleClick);
        QuitGOButton.onClick.AddListener(OnQuitClick);

        NextLevelButton.onClick.AddListener(OnNextLevelClick);
    }

    private void Start()
    {
        HidePauseScreen();
        HideGameOverScreen();
        HideVictoryScreen();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!GamePaused)
            {
                PauseGame();
            }
            else
            {
                UnpauseGame();
            }
        }

        //debug
        if (Input.GetKeyUp(KeyCode.G)) ShowGameOverScreen();
        if (Input.GetKeyUp(KeyCode.V) && (!victoryScreen.activeSelf)) ShowVictoryScreen();

    }

    private void FixedUpdate()
    {
        VictoryBlinkingEffect();
    }

    private void VictoryBlinkingEffect()
    {
        if (_isVictory)
        {
        //    hue += 3f;
        //    hue = Mathf.Clamp(hue, 0f, 360f);
        //    victoryFront.color = Color.HSVToRGB(hue, 70f, 90f);
        }
    }

    // via input
    public void PauseGame()
    {
        GamePaused = true;
        ShowPauseScreen();
        Time.timeScale = 0f;
    }

    // input and buttons
    public void UnpauseGame()
    {
        GamePaused = false;
        HidePauseScreen();
        Time.timeScale = 1f;
    }

    public void OnResumeClick()
    {
        UnpauseGame();
    }

    public void OnRestartClick()
    {
        UnpauseGame();
        sceneManager.RestartLevel();
    }

    public void OnBackToTitleClick()
    {
        UnpauseGame();
        sceneManager.ReturnToMenu();
    }

    public void OnQuitClick()
    {
        sceneManager.ExitGame();
    }

    public void OnNextLevelClick()
    {
        ///
    }

    //------------ Screens ----------------

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void HidePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        gameoverScreen.SetActive(true);
    }

    public void HideGameOverScreen()
    {
        gameoverScreen.SetActive(false);
    }

    public void ShowVictoryScreen()
    {
        //for gfx blinking text ))
        _isVictory = true;
        victoryScreen.SetActive(true);
    }

    public void HideVictoryScreen()
    {
        _isVictory = false;
        victoryScreen.SetActive(false);
    }
}
