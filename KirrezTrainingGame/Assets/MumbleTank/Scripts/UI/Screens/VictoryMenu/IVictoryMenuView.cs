using System;

namespace TankGame
{
    public interface IVictoryMenuView : IView
    {
        event Action NextLevelClicked;
    }
}