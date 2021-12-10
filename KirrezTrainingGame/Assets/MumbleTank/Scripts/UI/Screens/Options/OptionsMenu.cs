using System;
using UnityEngine;

namespace TankGame
{
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

            _view.SetHealth(_playerSettings.StartHealth);
        }

        private void OnBackClicked()
        {
            BackClicked();
        }

        private void OnDecreaseLivesClicked()
        {
            var health = _playerSettings.StartHealth - 1;
            health = Mathf.Clamp(health, _playerSettings.MinHealth, _playerSettings.MaxHealth);

            _playerSettings.StartHealth = health;
            _view.SetHealth(health);
        }

        private void OnAddLivesClicked()
        {
            var health = _playerSettings.StartHealth + 1;
            health = Mathf.Clamp(health, _playerSettings.MinHealth, _playerSettings.MaxHealth);

            _playerSettings.StartHealth = health;
            _view.SetHealth(health);
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
}