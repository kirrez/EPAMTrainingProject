using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Level3 : MonoBehaviour
    {
        public Transform StartPosition;
        public float TimeToSurvive;

        private IGame _game;
        private IGameHUD _gameHUD;
        private IUnitRepository _unitRepository;
        private ILocalizationManager _localizationManager;
        private string _taskDescription;
        private float _timer;

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

            _taskDescription = _localizationManager.GetText(LocalizationKeys.SurviveTime);
            _timer = TimeToSurvive;
            SetTaskDescription(_taskDescription, _timer);
        }

        private void FixedUpdate()
        {
            _timer -= Time.fixedDeltaTime;
            SetTaskDescription(_taskDescription, _timer);
            if (_timer <= 0)
            {
                _game.Win();
            }
        }

        public void SetTaskDescription(string description, float value)
        {
            _gameHUD.SetTaskDescription(description, value);
        }

    }
}