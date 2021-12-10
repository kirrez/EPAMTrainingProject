using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class VictoryMenu : IVictoryMenu
    {
        public event Action Proceeding = () => { };

        private IVictoryMenuView _view;

        public VictoryMenu()
        {
            var uiRoot = ServiceLocator.GetUIRoot();
            var resourceManager = ServiceLocator.GetResourceManager();

            _view = resourceManager.CreatePrefab<IVictoryMenuView, Views>(Views.Victory);
            _view.SetParent(uiRoot.MenuCanvas);

            _view.NextLevelClicked += OnNextLevelClicked;
        }

        private void OnNextLevelClicked()
        {
            Proceeding.Invoke();
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
