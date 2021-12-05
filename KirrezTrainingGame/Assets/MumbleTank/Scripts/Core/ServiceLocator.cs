using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static IGame Game;
    private static IPlayer Player;
    private static IUIRoot UIRoot;
    private static IGameHUD GameHUD;
    private static IGameCamera GameCamera;
    private static IUnitRepository UnitRepository;
    private static GameObject EventSystemContainer;

    private static IGameSettings GameSettings;
    private static IPlayerSettings PlayerSettings;
    private static IResourceManager ResourceManager;
    private static ILocalizationManager LocalizationManager;


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

    public static IGameHUD GetGameHUD()
    {
        if (GameHUD == null)
        {
            GameHUD = new GameHUD();
        }
        return GameHUD;
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

    public static IGameSettings GetGameSettings()
    {
        if (GameSettings == null)
        { 
            GameSettings = new GameSettings();
        }
        return GameSettings;
    }

    public static ILocalizationManager GetLocalizationManager()
    {
        if (LocalizationManager == null)
        {
            LocalizationManager = new LocalizationManager();
        }
        return LocalizationManager;
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



    private void OnDestroy()
    {
        Game = null;
        UIRoot = null;
        Player = null;
        GameHUD = null;
        GameCamera = null;

        UnitRepository = null;
        EventSystemContainer = null;
    }
}