using UnityEngine;

namespace TankGame
{
    public interface IGameCamera
    {
        Vector3 Position { get; }

        void SetTarget(Transform target);
        Ray ScreenPointToRay(Vector2 position);
        Vector3 WorldToViewportPoint(Vector3 position);
    }
}
