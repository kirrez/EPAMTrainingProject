using System;
using UnityEngine;

namespace TankGame
{
    public interface IPlayer
    {
        event Action<int> HealthChanged;
        event Action<int> MaxHealthChanged;

        event Action<bool> ShieldChanged;
        event Action<float> ShieldProgress;

        event Action Killed;

        int Health { get; }
        int MaxHealth { get; }

        Transform GetTransform();
        void SetStartPosition(Vector3 pos);
    }
}