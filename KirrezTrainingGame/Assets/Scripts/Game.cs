using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public PlayerShooting Player;
    public List<EnemySpawner> EnemySpawners;

    private GameObject _gameCamera;
    private Canvas _enemyCanvas;

    private void Awake()
    {
        //Camera Setup
        var cameraPrefab = Resources.Load<GameObject>("Prefabs/MainCamera");
        _gameCamera = Instantiate(cameraPrefab);
        var enemyCanvasPrefab = Resources.Load<GameObject>("Prefabs/UI/EnemyCanvas");
        _enemyCanvas = Instantiate(enemyCanvasPrefab.GetComponent<Canvas>());

        foreach (var spawner in EnemySpawners)
        {
            spawner.EnemySpawned += OnEnemySpawned;
        }
    }

    private void Start()
    {
        AttachCameraToPlayer();
    }

    private void OnEnemySpawned(Enemy enemy)
    {
        enemy.SetTarget(Player.transform);
        enemy.EnemyCanvas = _enemyCanvas;
    }

    private void AttachCameraToPlayer()
    {
        _gameCamera.GetComponent<CameraControl>().SetTarget(Player.transform);
        Player.GetComponent<PlayerMovement>().GetCamera(_gameCamera.GetComponent<Camera>());
    }

    public Camera GetCamera()
    {
        return _gameCamera.GetComponent<Camera>();
    }
}
