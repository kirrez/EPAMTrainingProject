using System;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class ResourceManager : IResourceManager
    {
        private List<PoolItem> ObjectPool = new List<PoolItem>();

        public GameObject GetFromPool<E>(E objType)
            where E : Enum
        {
            PoolItem unit;
            unit.type = objType.GetType();
            unit.value = objType;

            if (ObjectPool.Count > 0)
            {
                foreach (PoolItem element in ObjectPool)
                {
                    if (element.type.Equals(unit.type) && element.value.Equals(unit.value) && element.item.activeInHierarchy == false)
                    {

                        unit.item = element.item;
                        unit.item.SetActive(true);
                        //Debug.Log($"{unit.type}.{unit.value} RETURNED!");
                        return unit.item;
                    }
                }
            }

                var path = unit.type.ToString() + "/" + unit.value.ToString();
                var asset = Resources.Load<GameObject>(path);
                var instance = GameObject.Instantiate(asset);


                unit.item = instance;
                ObjectPool.Add(unit);
                //Debug.Log($"{unit.type}.{unit.value} CREATED! POOL : {ObjectPool.Count}");
                return unit.item;
        }

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
}