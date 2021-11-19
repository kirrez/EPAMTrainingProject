using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static IPlayer Player;
    private static IGameUI GameUI;
    private static IPlayerHUD PlayerHUD;
    private static IGameCamera GameCamera;
    private static IOverlayCanvas OverlayCanvas;
    private static IResourceManager ResourceManager;

    public static IGameCamera GetGameCamera()
    {
        if (GameCamera == null)
        {
            var resourceManager = GetResourceManager();

            GameCamera = resourceManager.CreatePrefab<IGameCamera, Components>(Components.GameCamera);
        }

        return GameCamera;
    }

    public static IPlayer GetPlayer()
    {
        if (Player == null)
        {
            var resourceManager = GetResourceManager();

            Player = resourceManager.CreatePrefab<IPlayer, Components>(Components.Player);
        }

        return Player;
    }

    public static IGameUI GatGameUI()
    {
        if (GameUI == null)
        {
            var resourceManager = GetResourceManager();

            GameUI = resourceManager.CreatePrefab<IGameUI, UIComponents>(UIComponents.GameUI);
        }

        return GameUI;
    }

    public static IPlayerHUD GetPlayerHUD()
    {
        if (PlayerHUD == null)
        {
            var resourceManager = GetResourceManager();

            PlayerHUD = resourceManager.CreatePrefab<IPlayerHUD, UIComponents>(UIComponents.PlayerHUD);
        }

        return PlayerHUD;
    }

    public static IOverlayCanvas GetOverlayCanvas()
    {
        if (OverlayCanvas == null)
        {
            var resourceManager = GetResourceManager();

            OverlayCanvas = resourceManager.CreatePrefab<IOverlayCanvas, UIComponents>(UIComponents.OverlayCanvas);
        }

        return OverlayCanvas;
    }

    public static IResourceManager GetResourceManager()
    {
        if (ResourceManager == null)
        {
            ResourceManager = new ResourceManager();
        }

        return ResourceManager;
    }

    private void OnDestroy()
    {
        ResourceManager = null;

        Player = null;
        GameUI = null;
        PlayerHUD = null;
        GameCamera = null;
        OverlayCanvas = null;
    }
}
