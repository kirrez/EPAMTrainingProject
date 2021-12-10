using UnityEngine;
using UnityEngine.SceneManagement;

namespace TankGame
{
    public class MainMenu : IMainMenu
    {
        private IMainMenuView _view;
        private IOptionsMenu _optionsMenu;

        public MainMenu()
        {
            var uiRoot = ServiceLocator.GetUIRoot();
            var eventSystem = ServiceLocator.GetEventSystem();
            var resourceManager = ServiceLocator.GetResourceManager();

            _view = resourceManager.CreatePrefab<IMainMenuView, Views>(Views.MainMenu);
            _view.SetParent(uiRoot.MenuCanvas);

            _view.StartGameClicked += OnStartGameClicked;
            _view.OptionsClicked += OnOptionsClicked;
            _view.QuitClicked += OnQuitClicked;

            _optionsMenu = new OptionsMenu();
            _optionsMenu.Hide();

            _optionsMenu.BackClicked += OnOptionsMenuBackClicked;
        }

        private void OnOptionsMenuBackClicked()
        {
            _optionsMenu.Hide();
            Show();
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }

        private void OnQuitClicked()
        {
            Application.Quit();
        }

        private void OnOptionsClicked()
        {
            Hide();
            _optionsMenu.Show();
        }

        private void OnStartGameClicked()
        {
            SceneManager.LoadScene(Levels.Level_1.ToString());
        }
    }
}