using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static IGame Game;
    private static IPlayer Player;
    private static IUIRoot UIRoot;
    private static IGameCamera GameCamera;
    private static GameObject LightContainer;
    private static IUnitRepository UnitRepository;
    private static GameObject EventSystemContainer;

    private static IPlayerSettings PlayerSettings;
    private static IResourceManager ResourceManager;
    

    public static IGame GetGame()
    {
        if (Game == null)
        {
            var resourceManager = GetResourceManager();

            Game = resourceManager.CreatePrefab<IGame, Components>(Components.Game);
        }
        return Game;
    }

    public static IUIRoot GetUIRoot()
    {
        if (UIRoot == null)
        {
            var resourceManager = GetResourceManager();

            UIRoot = resourceManager.CreatePrefab<IUIRoot, Components>(Components.UIRoot);
        }

        return UIRoot;
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

    public static IUnitRepository GetUnitRepository()
    {
        if (UnitRepository == null)
        {
            var resourceManager = GetResourceManager();

            UnitRepository = resourceManager.CreatePrefab<IUnitRepository, Components>(Components.UnitRepository);
        }

        return UnitRepository;
    }

    public static IPlayerSettings GetPlayerSettings()
    {
        if (PlayerSettings == null)
        {
            PlayerSettings = new PlayerSettings();
        }

        return PlayerSettings;
    }

    public static IResourceManager GetResourceManager()
    {
        if (ResourceManager == null)
        {
            ResourceManager = new ResourceManager();
        }

        return ResourceManager;
    }

    public static GameObject GetEventSystem()
    {
        if (EventSystemContainer == null)
        {
            var resourceManager = GetResourceManager();

            EventSystemContainer = resourceManager.CreatePrefab<Components>(Components.EventSystemContainer);
        }
        return EventSystemContainer;
    }

    public static GameObject GetLight()
    {
        if (LightContainer == null)
        {
            var resourceManager = GetResourceManager();

            LightContainer = resourceManager.CreatePrefab<Components>(Components.LightContainer);
        }
        return LightContainer;
    }

    private void OnDestroy()
    {
        Game = null;
        UIRoot = null;
        Player = null;
        GameCamera = null;
        LightContainer = null;
        UnitRepository = null;
        EventSystemContainer = null;
    }
}