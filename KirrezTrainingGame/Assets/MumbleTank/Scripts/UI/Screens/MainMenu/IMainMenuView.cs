using System;

namespace TankGame
{
    public interface IMainMenuView : IView
    {
        event Action StartGameClicked;
        event Action OptionsClicked;
        event Action QuitClicked;
    }
}