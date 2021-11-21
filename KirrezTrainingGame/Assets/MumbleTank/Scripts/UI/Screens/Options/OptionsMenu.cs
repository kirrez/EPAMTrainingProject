using System;
using UnityEngine;

public class OptionsMenu : IOptionsMenu
{
    public event Action BackClicked = () => { };

    private IOptionsMenuView _view;
    private IPlayerSettings _playerSettings;

    public OptionsMenu()
    {
        var uiRoot = ServiceLocator.GetUIRoot();
        var resourceManager = ServiceLocator.GetResourceManager();
        _playerSettings = ServiceLocator.GetPlayerSettings();

        _view = resourceManager.CreatePrefab<IOptionsMenuView, Views>(Views.OptionsMenu);
        _view.SetParent(uiRoot.MenuCanvas);

        _view.BackClicked += OnBackClicked;
        _view.AddLivesClicked += OnAddLivesClicked;
        _view.DecreaseLivesClicked += OnDecreaseLivesClicked;
    }

    private void OnBackClicked()
    {
        BackClicked();
    }

    private void OnDecreaseLivesClicked()
    {
        _playerSettings.SetHitpoints(false);

        var maxHitpoints = _playerSettings.GetMaxHitpoints();
        _view.SetLivesAmount(maxHitpoints);
    }

    private void OnAddLivesClicked()
    {
        _playerSettings.SetHitpoints(true);

        var maxHitpoints = _playerSettings.GetMaxHitpoints();
        _view.SetLivesAmount(maxHitpoints);
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