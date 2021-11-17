using System;
using UnityEngine;

public class ResourceManager : IResourceManager
{
    public T CreatePrefab<T, E>(E type)
        where E : Enum
    {
        var path = type.GetType().Name + "/" + type.ToString();
        var asset = Resources.Load<GameObject>(path);
        var instance = GameObject.Instantiate(asset);
        var component = instance.GetComponent<T>();

        return component;
    }

    public GameObject CreatePrefab<E>(E type)
        where E : Enum
    {
        var path = type.GetType().Name + "/" + type.ToString();
        var asset = Resources.Load<GameObject>(path);
        var instance = GameObject.Instantiate(asset);

        return instance;
    }
}
