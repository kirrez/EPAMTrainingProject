using UnityEngine;
using System;

public interface IEnemy
{
    event Action<IEnemy> Died;

    Vector3 Position { get; set; }
    Quaternion Rotation { get; set; }

    int GetScore();
    bool IsEnabled();
    void Enable();
}
