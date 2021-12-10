using UnityEngine;
using UnityEngine.UI;

namespace TankGame
{
    public class MenuScene : MonoBehaviour
    {
        private IMainMenu _mainMenu;
        private IOptionsMenu _optionsMenu;

        private IPlayerSettings _playerSettings;

        private void Awake()
        {
            _playerSettings = ServiceLocator.GetPlayerSettings();

            _mainMenu = new MainMenu();

        }

        private void Start()
        {
            _mainMenu.Show();
        }
    }
}
