using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public int KillCount;

    public Transform StartPosition;

    private IGame _game;
    private IGameHUD _gameHUD;
    private IUnitRepository _unitRepository;
    private ILocalizationManager _localizationManager;
    private string _taskDescription;
    private int _killCount;

    private void Awake()
    {
        var player = ServiceLocator.GetPlayer();
        var camera = ServiceLocator.GetGameCamera();
        var resourceManager = ServiceLocator.GetResourceManager();

        _game = ServiceLocator.GetGame();
        _gameHUD = ServiceLocator.GetGameHUD();

        _unitRepository = ServiceLocator.GetUnitRepository();
        _localizationManager = ServiceLocator.GetLocalizationManager();

        camera.SetTarget(player.GetTransform());
        player.SetStartPosition(StartPosition.position);
        _taskDescription = _localizationManager.GetText(LocalizationKeys.KillEnemies);
        SetTaskDescription(_taskDescription, _killCount, KillCount);

        _unitRepository.Killed += CheckKillingEnemiesVictory;
    }

    public void CheckKillingEnemiesVictory(IEnemy enemy)
    {
        _killCount++;
        SetTaskDescription(_taskDescription, _killCount, KillCount);
        if (KillCount == _killCount)
        {
            _game.Win();
        }
    }

    public void SetTaskDescription(string description, int current, int maximum)
    {
        _gameHUD.SetTaskDescription(description, current, maximum);
    }

}
