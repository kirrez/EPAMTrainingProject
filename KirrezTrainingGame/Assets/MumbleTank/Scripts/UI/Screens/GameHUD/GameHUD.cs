using UnityEngine;

public class GameHUD : IGameHUD
{
    private IGameHUDView _view;

    public GameHUD()
    {
        var player = ServiceLocator.GetPlayer();
        var uiRoot = ServiceLocator.GetUIRoot();
        var resourceManager = ServiceLocator.GetResourceManager();

        _view = resourceManager.CreatePrefab<IGameHUDView, Views>(Views.GameHUD);
        _view.SetParent(uiRoot.MainCanvas);

        player.ShieldChanged += OnPlayerShieldChanged;
        player.ShieldProgress += OnPlayerShieldProgress;

        player.HealthChanged += OnPlayerHealthChanged;
        player.MaxHealthChanged += OnPlayerMaxHealthChanged;
    }

    private void OnPlayerHealthChanged(int value)
    {
        _view.SetHealth(value);
    }

    private void OnPlayerMaxHealthChanged(int value)
    {
        _view.SetMaxHealth(value);
    }

    private void OnPlayerShieldProgress(float normalizedValue)
    {
        _view.SetShieldProgress(normalizedValue);
    }

    private void OnPlayerShieldChanged(bool isActive)
    {
        _view.SetShieldActive(isActive);
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