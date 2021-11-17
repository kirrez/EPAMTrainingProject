using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    private static IGameCamera GameCamera;
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

    public static IResourceManager GetResourceManager()
    {
        if (ResourceManager == null)
        {
            ResourceManager = new ResourceManager();
        }

        return ResourceManager;
    }
}
