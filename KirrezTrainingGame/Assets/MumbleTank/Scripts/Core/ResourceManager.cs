using UnityEngine;

public class ResourceManager : IResourceManager
{
    T IResourceManager.CreatePrefab<T, E>(E type)
    {
        var path = type.GetType().Name + "/" + type.ToString();
        var asset = Resources.Load<GameObject>(path);
        var instance = GameObject.Instantiate(asset);
        var component = instance.GetComponent<T>();

        return component;
    }
}
