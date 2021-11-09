using System;

public interface IPlayer
{
    event Action<int> HealthChanged;
}
