using System;
using UnityEngine.UI;

public class GameOverMenuView : BaseView, IGameOverMenuView
{
    public event Action RestartClicked = () => { };
    public event Action BackClicked = () => { };
    public event Action QuitClicked = () => { };

    public Button RestartButton;
    public Button BackButton;
    public Button QuitButton;

    private void Awake()
    {
        RestartButton.onClick.AddListener(OnRestartClick);
        BackButton.onClick.AddListener(OnBackClick);
        QuitButton.onClick.AddListener(OnQuitClick);
    }

    public void OnRestartClick()
    {
        RestartClicked();
    }

    public void OnBackClick()
    {
        BackClicked();
    }

    public void OnQuitClick()
    {
        QuitClicked();
    }
}
