using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : IVictoryMenu
{
    private IVictoryMenuView _view;

    public VictoryMenu()
    {
        var uiRoot = ServiceLocator.GetUIRoot();
        var resourceManager = ServiceLocator.GetResourceManager();

        _view = resourceManager.CreatePrefab<IVictoryMenuView, Views>(Views.Victory);
        _view.SetParent(uiRoot.MenuCanvas);

        _view.ResumeClicked += OnResumeClicked;
        _view.NextLevelClicked += OnNextLevelClicked;
    }

    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }

    private void OnResumeClicked()
    {
        Hide();
    }

    private void OnNextLevelClicked()
    {
        // no functionality right now
        Hide();
    }
}
