using System;
using UnityEngine;

public interface IResourceManager
{
    GameObject CreatePrefab<E>(E type) where E : Enum;
    T CreatePrefab<T, E>(E type) where E : Enum;
}