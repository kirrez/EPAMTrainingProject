using System;
using UnityEngine;

namespace TankGame
{
    public interface IResourceManager
    {
        GameObject GetFromPool<E>(E objType) where E : Enum;
        GameObject CreatePrefab<E>(E type) where E : Enum;
        T CreatePrefab<T, E>(E type) where E : Enum;
    }
}