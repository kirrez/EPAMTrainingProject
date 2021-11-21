using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static IPlayer Player;
    private static IGameUI GameUI;
    private static IUIRoot UIRoot;
    private static IPlayerHUD PlayerHUD;
    private static IGameCamera GameCamera;
    private static IPlayerSettings PlayerSettings;
    private static IResourceManager ResourceManager;

    public static IUIRoot GetUIRoot()
    {
        if (UIRoot == null)
        {
            var resourceManager = GetResourceManager();

            UIRoot = resourceManager.CreatePrefab<IUIRoot, Components>(Components.UIRoot);
        }

        return UIRoot;
    }

    public static IPlayerSettings GetPlayerSettings()
    {
        if (PlayerSettings == null)
        {
            PlayerSettings = new PlayerSettings();
        }

        return PlayerSettings;
    }

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

        UIRoot = null;
        Player = null;
        GameUI = null;
        PlayerHUD = null;
        GameCamera = null;
    }
}