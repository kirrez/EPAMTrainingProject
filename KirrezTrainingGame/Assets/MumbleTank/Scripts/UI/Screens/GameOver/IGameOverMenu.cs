using System;

namespace TankGame
{
    public interface IGameOverMenu : IScreen
    {
        event Action Restarting;
        event Action Backing;
        event Action Quitting;
    }
}