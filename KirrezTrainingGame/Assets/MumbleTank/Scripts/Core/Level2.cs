using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class Level2 : MonoBehaviour
    {
        public Transform StartPosition;
        public HelicopterSite HelicopterSite;

        private IGame _game;
        private IGameHUD _gameHUD;
        private IUnitRepository _unitRepository;
        private ILocalizationManager _localizationManager;
        private string _taskDescription;

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
            _taskDescription = _localizationManager.GetText(LocalizationKeys.ReachDestination);
            SetTaskDescription(_taskDescription);

            HelicopterSite.IsReached += CheckDestinationReached;
        }

        public void CheckDestinationReached()
        {
            _game.Win();
        }

        public void SetTaskDescription(string description)
        {
            _gameHUD.SetTaskDescription(description);
        }

    }
}