using System;
using UnityEngine;

public interface IPlayer
{
    event Action<int> HealthChanged;
    event Action Killed;

    Transform GetTransform();
    void SetStartPosition(Vector3 pos);
}
