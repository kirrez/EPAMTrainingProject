using System;

namespace TankGame
{
    public interface IGameOverMenuView : IView
    {
        event Action RestartClicked;
        event Action BackClicked;
        event Action QuitClicked;
    }
}