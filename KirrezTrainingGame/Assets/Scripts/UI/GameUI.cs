using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public bool GamePaused { get; set; } = false;

    public GameObject pauseScreen;
    public GameObject gameoverScreen;
    public GameObject victoryScreen;
    // text blinking effect
    public Image victoryFront;
    public Image victoryBack;
    //private float hue = 0f;
    private bool _victoryActivated = false;

    private void Start()
    {
        ActivatePauseScreen(false);
        ActivateGameoverScreen(false);
        ActivateVictoryScreen(false);
        //victoryFront.color = new Color(255f, 0f, 0f, 255f);
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
        if (Input.GetKeyUp(KeyCode.T)) ActivateGameoverScreen(true);
        if (Input.GetKeyUp(KeyCode.Y) && (!victoryScreen.activeSelf)) ActivateVictoryScreen(true);

    }

    private void FixedUpdate()
    {
        //if (_victoryActivated)
        //{
        //    hue += 3f;
        //    hue = Mathf.Clamp(hue, 0f, 360f);
        //    victoryFront.color = Color.HSVToRGB(hue, 70f, 90f);
        //}
    }

    // for buttons
    public void ActivateGameoverScreen(bool activate)
    {
        gameoverScreen.SetActive(activate);
    }

    // for buttons
    public void ActivateVictoryScreen(bool activate)
    {
        _victoryActivated = activate;
        victoryScreen.SetActive(activate);
    }

    public void ActivateGameoverScreen()
    {
        gameoverScreen.SetActive(true);
    }

    public void ActivatePauseScreen(bool activate)
    {
        pauseScreen.SetActive(activate);
    }

    // via input
    public void PauseGame()
    {
        GamePaused = true;
        ActivatePauseScreen(true);
        Time.timeScale = 0f;
    }

    // input and buttons
    public void UnpauseGame()
    {
        GamePaused = false;
        ActivatePauseScreen(false);
        Time.timeScale = 1f;
    }

}
