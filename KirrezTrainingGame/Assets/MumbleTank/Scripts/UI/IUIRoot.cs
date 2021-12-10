using UnityEngine;

namespace TankGame
{
    public interface IUIRoot
    {
        Transform OverlayCanvas { get; }
        Transform MainCanvas { get; }
        Transform MenuCanvas { get; }
    }
}