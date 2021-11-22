using System;
using UnityEngine.UI;

public class VictoryMenuView : BaseView, IVictoryMenuView
{
    public event Action ResumeClicked = () => { };
    public event Action NextLevelClicked = () => { };

    public Button ResumeButton;
    public Button NextLevelButton;

    private void Awake()
    {
        ResumeButton.onClick.AddListener(OnResumeClick);
        NextLevelButton.onClick.AddListener(OnNextLevelClick);
    }

    public void OnResumeClick()
    {
        ResumeClicked();
    }

    public void OnNextLevelClick()
    {
        NextLevelClicked();
    }
}
