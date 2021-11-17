using System;
using UnityEngine;

public interface IResourceManager
{
    T CreatePrefab<T, E>(E type) where E : Enum;
}