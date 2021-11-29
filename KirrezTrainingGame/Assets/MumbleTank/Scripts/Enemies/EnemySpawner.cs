using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
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
    private List<IEnemy> _enemies = new List<IEnemy>();

    private IResourceManager _resourceManager;
    private IUnitRepository _unitRepository;

    private void Awake()
    {
        _resourceManager = ServiceLocator.GetResourceManager();
        _unitRepository = ServiceLocator.GetUnitRepository();

        _positionX = Boundary.position.x;
        _positionZ = Boundary.position.z;
        _scaleX = Boundary.localScale.x / 2;
        _scaleZ = Boundary.localScale.z / 2;
    }

    private void FixedUpdate()
    {
        if (_currentTime <= 0)
        {
            AddNewEnemy();
            CallExistingEnemy();
            _currentTime = SpawnPeriod;
        }

        _currentTime -= Time.fixedDeltaTime;
    }

    private void CallExistingEnemy()
    {
        if (_enemies.Count >= MaxEnemies)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy.IsEnabled() == false)
                {
                    enemy.Enable();
                    _unitRepository.Register(enemy);
                    return;
                }
            }
        }
    }

    private void AddNewEnemy()
    {
        if (_enemies.Count < MaxEnemies)
        {
            var newX = _positionX + UnityEngine.Random.Range(-_scaleX, _scaleX);
            var newZ = _positionZ + UnityEngine.Random.Range(-_scaleZ, _scaleZ);
            var newY = BaseEnemy.CorrectYPosition(Type);

            var enemy = _resourceManager.CreatePrefab<IEnemy, EnemyType>(Type);
            enemy.Position = new Vector3(newX, newY, newZ);
            enemy.Rotation = Quaternion.identity;

            _enemies.Add(enemy);

            enemy.Died += OnEnemyDied;

            _unitRepository.Register(enemy);
        }
    }

    private void OnEnemyDied(IEnemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        _killedEnemiesNumber++;
        _unitRepository.Unregister(enemy);
    }
}
