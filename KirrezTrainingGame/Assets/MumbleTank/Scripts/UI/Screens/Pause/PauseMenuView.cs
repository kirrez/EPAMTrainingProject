using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenuView : BaseView, IPauseMenuView
{
    public event Action ResumeClicked = () => { };
    public event Action RestartClicked = () => { };
    public event Action BackClicked = () => { };

    public Button ResumeButton;
    public Button RestartButton;
    public Button BackButton;

    private void Awake()
    {
        ResumeButton.onClick.AddListener(OnResumeClick);
        RestartButton.onClick.AddListener(OnRestartClick);
        BackButton.onClick.AddListener(OnBackClick);
    }

    public void OnResumeClick()
    {
        ResumeClicked();
    }

    public void OnRestartClick()
    {
        RestartClicked();
    }

    public void OnBackClick()
    {
        BackClicked();
    }
}
