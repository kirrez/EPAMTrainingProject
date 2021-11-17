using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action<Enemy> EnemySpawned;

    public EnemyType Type;
    public Transform Boundary;

    public int MaxEnemies;
    public float SpawnPeriod = 2f;

    private float _currentTime = 0f;
    private float _positionX;
    private float _positionZ;
    private float _scaleX; // 1/2 of X scale
    private float _scaleZ; // 1/2 of Z scale

    private int _killedEnemiesNumber;
    private List<Enemy> _enemies = new List<Enemy>();

    private IResourceManager _resourceManager;

    private void Awake()
    {
        _resourceManager = ServiceLocator.GetResourceManager();

        _positionX = Boundary.position.x;
        _positionZ = Boundary.position.z;
        _scaleX = Boundary.localScale.x / 2;
        _scaleZ = Boundary.localScale.z / 2;
    }

    private void FixedUpdate()
    {
        if (_currentTime <= 0 && _enemies.Count < MaxEnemies)
        {
            _currentTime = SpawnPeriod;
            var newX = _positionX + UnityEngine.Random.Range(-_scaleX, _scaleX);
            var newZ = _positionZ + UnityEngine.Random.Range(-_scaleZ, _scaleZ);
            var newY = Enemy.CorrectYPosition(Type);
            // adding new enemy to scene if not reached their maximum
            AddEnemy(newX, newY, newZ);
        }
        else
        {
            _currentTime -= Time.fixedDeltaTime;
        }
    }

    private void AddEnemy(float X, float Y, float Z)
    {
        var enemy = _resourceManager.CreatePrefab<Enemy, EnemyType>(Type);
        enemy.transform.position = new Vector3(X, Y, Z);
        enemy.transform.rotation = Quaternion.identity;
        enemy.transform.SetParent(transform);

        _enemies.Add(enemy);

        EnemySpawned(enemy);
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        _killedEnemiesNumber++;
    }
}
