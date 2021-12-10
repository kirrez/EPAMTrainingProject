using System;

namespace TankGame
{
    public interface IOptionsMenu : IScreen
    {
        event Action BackClicked;
    }
}