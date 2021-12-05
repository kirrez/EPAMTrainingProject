using System;
using UnityEngine.UI;

public class VictoryMenuView : BaseView, IVictoryMenuView
{
    public event Action NextLevelClicked = () => { };

    public Button NextLevelButton;

    private void Awake()
    {
        NextLevelButton.onClick.AddListener(OnNextLevelClick);
    }

    public void OnNextLevelClick()
    {
        NextLevelClicked();
    }
}
