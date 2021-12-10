using System;

namespace TankGame
{
    public interface IOptionsMenuView : IView
    {
        event Action AddLivesClicked;
        event Action DecreaseLivesClicked;
        event Action BackClicked;

        void SetHealth(int value);
    }
}