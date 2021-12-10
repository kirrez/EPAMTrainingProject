using System;

namespace TankGame
{
    public interface IHealth
    {
        event Action<float> HealthChanged;
        event Action<float> MaxHealthChanged;

        float MaxHealth { get; }
        float CurrentHealth { get; }
    }
}
