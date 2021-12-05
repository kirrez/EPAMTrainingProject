using UnityEngine;
using System;

public class PauseMenu : IPauseMenu
{
    public event Action Resuming = () => { };
    public event Action Restarting = () => { };
    public event Action Backing = () => { };

    private IPauseMenuView _view;

    public PauseMenu()
    {
        var uiRoot = ServiceLocator.GetUIRoot();
        var resourceManager = ServiceLocator.GetResourceManager();

        _view = resourceManager.CreatePrefab<IPauseMenuView, Views>(Views.Pause);
        _view.SetParent(uiRoot.MenuCanvas);

        _view.ResumeClicked += OnResumeClicked;
        _view.RestartClicked += OnRestartClicked;
        _view.BackClicked += OnBackClicked;
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
        Resuming.Invoke();
    }

    private void OnRestartClicked()
    {
        Restarting.Invoke();
    }

    private void OnBackClicked()
    {
        Backing.Invoke();
    }
}
