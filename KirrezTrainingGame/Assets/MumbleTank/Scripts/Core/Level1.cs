using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public Transform StartPosition;

    private IGame _game;
    private IUnitRepository _unitRepository;

    private void Awake()
    {
        var player = ServiceLocator.GetPlayer();
        var camera = ServiceLocator.GetGameCamera();

        _game = ServiceLocator.GetGame();
        _unitRepository = ServiceLocator.GetUnitRepository();

        camera.SetTarget(player.GetTransform());
        player.SetStartPosition(StartPosition.position);
    }

}
