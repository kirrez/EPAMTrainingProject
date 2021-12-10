using System;

namespace TankGame
{
    public interface IUnitRepository
    {
        event Action<IEnemy> Killed;

        void Register(IEnemy value);
        void Unregister(IEnemy value);
        void StopChasingPlayer();
    }
}