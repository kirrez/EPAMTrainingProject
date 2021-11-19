using UnityEngine;
using System;

public interface IEnemy
{
    event Action<IEnemy> Died;

    Vector3 Position { get; set; }
    Quaternion Rotation { get; set; }

    void GetCanvas(IOverlayCanvas canvas);
    void SetTarget(Transform target);
}
