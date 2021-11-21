using UnityEngine;
using UnityEngine.UI;

public class MenuScene : MonoBehaviour
{
    public float SplashMovementPeriod = 4f;
    public float FadePeriod = 2.5f;
    private float _splashOffset = 1200f;
    private float _currentTime = 0f;
    private float _currentFadeTime = 0f;
    private float distance = 0f;
    private float startYposition = 0f;
    private Color fadeColor;
    private MySceneManagment sceneManager;

    //public Image fade;
    //public RectTransform splashScreen;
    //public Text livesAmount;

    private IMainMenu _mainMenu;
    private IOptionsMenu _optionsMenu;

    private IPlayerSettings _playerSettings;

    private void Awake()
    {
        _playerSettings = ServiceLocator.GetPlayerSettings();

        _mainMenu = new MainMenu();
        
    }

    private void Start()
    {
        _mainMenu.Show();

        //_optionsMenu.Hide();

        //ActivateSplash();
        //UpdateLivesAmountText();
    }

    //private void FixedUpdate()
    //{
    //    //// splash screen movement
    //    //if (_currentTime > 0)
    //    //{
    //    //    distance = startYposition - (_currentTime / SplashMovementPeriod) * _splashOffset;
    //    //    splashScreen.position = new Vector3(splashScreen.position.x, distance, 0f);
    //    //    _currentTime -= Time.fixedDeltaTime;
    //    //}
    //    //// Fade-in effect
    //    //if (_currentFadeTime > 0)
    //    //{
    //    //    fadeColor.a = _currentFadeTime / FadePeriod;
    //    //    fade.color = fadeColor;
    //    //    _currentFadeTime -= Time.fixedDeltaTime;
    //    //}
    //}

    //public void OnStartGameClick()
    //{
    //    sceneManager.StartNewGame();
    //}

    //public void OnOptionsClick()
    //{
    //    //HideMainMenu();
    //    ShowOptions();
    //}

    //public void OnQuitClick()
    //{
    //    sceneManager.ExitGame();
    //}

    //public void OnAddLivesClick()
    //{
    //    playerSettings.SetHitpoints(true);
    //    UpdateLivesAmountText();
    //}

    //public void OnDecreaseLivesClick()
    //{
    //    playerSettings.SetHitpoints(false);
    //    UpdateLivesAmountText();
    //}

    //public void OnBackToMenuClick()
    //{
    //    HideOptions();
    //    //ShowMainMenu();
    //}

    //private void UpdateLivesAmountText()
    //{
    //    livesAmount.text = playerSettings.GetMaxHitpoints().ToString();
    //}

    //private void ActivateSplash()
    //{
    //    startYposition = splashScreen.position.y;
    //    distance = splashScreen.position.y - _splashOffset;
    //    splashScreen.position = new Vector3(splashScreen.position.x, distance, 0f);
    //    _currentTime = SplashMovementPeriod;
    //    _currentFadeTime = FadePeriod;
    //    fadeColor = fade.color;
    //}

    //public void ShowMainMenu()
    //{
    //    MainMenu.SetActive(true);
    //}

    //public void HideMainMenu()
    //{
    //    MainMenu.SetActive(false);
    //}

    //public void ShowOptions()
    //{
    //    //Options.SetActive(true);
    //}

    //public void HideOptions()
    //{
    //    //Options.SetActive(false);
    //}
}
