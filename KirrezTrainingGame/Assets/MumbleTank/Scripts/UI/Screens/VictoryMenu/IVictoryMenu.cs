using System;

namespace TankGame
{
    public interface IVictoryMenu : IScreen
    {
        event Action Proceeding;
    }
}