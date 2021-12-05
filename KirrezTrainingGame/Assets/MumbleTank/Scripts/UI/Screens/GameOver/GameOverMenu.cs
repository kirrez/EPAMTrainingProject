using System;
using UnityEngine;

public class GameOverMenu : IGameOverMenu
{
    public event Action Restarting = () => { };
    public event Action Backing = () => { };
    public event Action Quitting = () => { };

    private IGameOverMenuView _view;

    public GameOverMenu()
    {
        var uiRoot = ServiceLocator.GetUIRoot();
        var resourceManager = ServiceLocator.GetResourceManager();

        _view = resourceManager.CreatePrefab<IGameOverMenuView, Views>(Views.GameOver);
        _view.SetParent(uiRoot.MenuCanvas);

        _view.RestartClicked += OnRestartClicked;
        _view.BackClicked += OnBackClicked;
        _view.QuitClicked += OnQuitClicked;
    }


    private void OnRestartClicked()
    {
        Restarting.Invoke();
    }

    private void OnBackClicked()
    {
        Backing.Invoke();
    }

    private void OnQuitClicked()
    {
        Quitting.Invoke();
    }

    public void Show()
    {
        _view.Show();
    }

    public void Hide()
    {
        _view.Hide();
    }
}
