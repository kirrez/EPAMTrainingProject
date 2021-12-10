using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class EnemySpawner : MonoBehaviour
    {
        public EnemyType Type;
        public Transform Boundary;

        public bool IsUnlimited;

        public int Count;
        public int MaxEnemiesOnScene;

        public float SpawnPeriod = 2f;

        private float _currentTime = 0f;
        private float _positionX;
        private float _positionZ;
        private float _scaleX; // 1/2 of X scale
        private float _scaleZ; // 1/2 of Z scale

        private List<IEnemy> _enemies = new List<IEnemy>();
        private List<IEnemy> _enemyPool = new List<IEnemy>();

        private IGameSettings _gameSettings;
        private IResourceManager _resourceManager;

        private int _enemiesSpawned;

        private void Awake()
        {
            _gameSettings = ServiceLocator.GetGameSettings();
            _resourceManager = ServiceLocator.GetResourceManager();

            _positionX = Boundary.position.x;
            _positionZ = Boundary.position.z;
            _scaleX = Boundary.localScale.x / 2;
            _scaleZ = Boundary.localScale.z / 2;
        }

        private void FixedUpdate()
        {
            if (_currentTime > 0)
            {
                _currentTime -= Time.fixedDeltaTime;
                return;
            }

            if (IsUnlimited == false && _enemiesSpawned >= Count)
            {
                return;
            }

            Spawn();

            _currentTime = SpawnPeriod;
        }

        private void Spawn()
        {
            IEnemy enemy;

            if (_enemyPool.Count > 0)
            {
                enemy = _enemyPool[0];
                _enemyPool.RemoveAt(0);
            }
            else
            {
                enemy = _resourceManager.CreatePrefab<IEnemy, EnemyType>(Type);
                enemy.Died += OnEnemyDied;
            }

            enemy.Position = GetStartPosition();
            enemy.Rotation = Quaternion.identity;

            _enemies.Add(enemy);
        }

        private Vector3 GetStartPosition()
        {
            var newX = _positionX + UnityEngine.Random.Range(-_scaleX, _scaleX);
            var newZ = _positionZ + UnityEngine.Random.Range(-_scaleZ, _scaleZ);
            var newY = _gameSettings.GetStartPositionY(Type);
            var newPosition = new Vector3(newX, newY, newZ);

            return newPosition;
        }

        private void OnEnemyDied(IEnemy enemy)
        {
            _enemies.Remove(enemy);
            _enemyPool.Add(enemy);
        }
    }
}
