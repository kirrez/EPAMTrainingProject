using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    public float SplashMovementPeriod = 4f;
    public float FadePeriod = 2.5f;
    private float _splashOffset = 1200f;
    private float _currentTime = 0f;
    private float _currentFadeTime = 0f;
    private float distance = 0f;
    private float startYposition = 0f;
    private Color fadeColor;
    private PlayerSettings playerSettings;

    public Image fade;
    public RectTransform splashScreen;
    public Text livesAmount;

    public GameObject MainMenu;
    public GameObject Options;

    private void UpdateLivesAmountText()
    {
        livesAmount.text = playerSettings.GetMaxHitpoints().ToString();
    }

    private void ActivateSplash()
    {
        startYposition = splashScreen.position.y;
        distance = splashScreen.position.y - _splashOffset;
        splashScreen.position = new Vector3(splashScreen.position.x, distance, 0f);
        _currentTime = SplashMovementPeriod;
        _currentFadeTime = FadePeriod;
        fadeColor = fade.color;
    }

    // for buttons
    public void ChangeLivesNumber(bool increase)
    {
        playerSettings.SetHitpoints(increase);
        UpdateLivesAmountText();
    }

    // for buttons
    public void ActivateMainMenu(bool activate)
    {
        MainMenu.SetActive(activate);
    }

    // for buttons
    public void ActivateOptions(bool activate)
    {
        Options.SetActive(activate);
    }

    private void Start()
    {
        ActivateMainMenu(true);
        ActivateOptions(false);
        ActivateSplash();
        playerSettings = gameObject.GetComponent<PlayerSettings>();
        UpdateLivesAmountText();
    }

    private void FixedUpdate()
    {
        // splash screen movement
        if (_currentTime > 0)
        {
            distance = startYposition - (_currentTime / SplashMovementPeriod) * _splashOffset;
            splashScreen.position = new Vector3(splashScreen.position.x, distance, 0f);
            _currentTime -= Time.fixedDeltaTime;
        }
        // Fade-in effect
        if (_currentFadeTime > 0)
        {
            fadeColor.a = _currentFadeTime / FadePeriod;
            fade.color = fadeColor;
            _currentFadeTime -= Time.fixedDeltaTime;
        }
    }
}
