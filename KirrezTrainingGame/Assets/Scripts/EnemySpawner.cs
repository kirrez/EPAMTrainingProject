using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action<Enemy> EnemySpawned;

    public EnemyType type;
    public Transform boundary;
    public Player player;
    public Transform target;

    public int maxActiveEnemies;
    public float spawnPeriod = 2f;

    private float _currentTime = 0f;
    private float positionX;
    private float positionZ;
    private float scaleX; // 1/2 of X scale
    private float scaleZ; // 1/2 of Z scale
    private Enemy enemyPrefab;

    private int KilledEnemiesNumber;

    private List<Enemy> _polledEnemies = new List<Enemy>(1);
    private List<Enemy> _activeEnemies = new List<Enemy>(1);

    private void Awake()
    {
        EnemyLoader();
        positionX = boundary.position.x;
        positionZ = boundary.position.z;
        scaleX = boundary.localScale.x / 2;
        scaleZ = boundary.localScale.z / 2;
    }

    private void FixedUpdate()
    {
        if (_currentTime <= 0)
        {
            _currentTime = spawnPeriod;
            var newX = positionX + UnityEngine.Random.Range(-scaleX, scaleX);
            var newZ = positionZ + UnityEngine.Random.Range(-scaleZ, scaleZ);
            var newY = Enemy.CorrectYPosition(type);
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
        Enemy enemy;

        var amount = EnemyListAmount();

        enemy = ReadyForSpawn();
        if (enemy != null)
        {
            enemy.gameObject.SetActive(true);
            enemy.GetComponent<Transform>().position = new Vector3(X, Y, Z);
        }
        else if (amount < maxActiveEnemies)
        {
            enemy = Instantiate<Enemy>(enemyPrefab, new Vector3(X, Y, Z), Quaternion.identity, transform);
            _activeEnemies.Add(enemy);
        }

        if (enemy != null)
        {
            EnemySpawned(enemy);
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;
        KilledEnemiesNumber++;
    }

    private int EnemyListAmount()
    {
        int amount = 0;
        foreach (var enemy in _activeEnemies)
        {
            amount++;
        }
        //Debug.Log($"amount : {amount}");
        return amount;
    }

    // returns first disabled enemy from list or null if all of them are enabled
    private Enemy ReadyForSpawn()
    {
        var number = 0;
        Enemy recruit = null;
        foreach (var enemy in _activeEnemies)
        {
            number++;
            if (enemy.isActiveAndEnabled == false)
            {
                recruit = enemy;
                break;
            }
        }
        return recruit;
    }

    private void EnemyLoader()
    {
        switch (type)
        {
            case EnemyType.Spikedmine :
                enemyPrefab = Resources.Load<Enemy>("Prefabs/Spikedmine");
                break;
            case EnemyType.Turret:
                enemyPrefab = Resources.Load<Enemy>("Prefabs/Turret");
                break;
            case EnemyType.Flyer:
                enemyPrefab = Resources.Load<Enemy>("Prefabs/Flyer");
                break;
            default :
                enemyPrefab = Resources.Load<Enemy>("Prefabs/Spikedmine");
                break;
        }
    }
}
