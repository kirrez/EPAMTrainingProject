using System;
using UnityEngine.UI;

namespace TankGame
{
    public class MainMenuView : BaseView, IMainMenuView
    {
        public event Action StartGameClicked = () => { };
        public event Action OptionsClicked = () => { };
        public event Action QuitClicked = () => { };

        public Button StartGameButton;
        public Button OptionsButton;
        public Button QuitButton;

        private void Awake()
        {
            StartGameButton.onClick.AddListener(OnStartGameClick);
            OptionsButton.onClick.AddListener(OnOptionsClick);
            QuitButton.onClick.AddListener(OnQuitClick);
        }

        public void OnStartGameClick()
        {
            StartGameClicked();
        }

        public void OnOptionsClick()
        {
            OptionsClicked();
        }

        public void OnQuitClick()
        {
            QuitClicked();
        }
    }
}