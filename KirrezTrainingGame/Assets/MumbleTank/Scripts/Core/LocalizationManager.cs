using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame
{
    public class LocalizationManager : ILocalizationManager
    {
        private Dictionary<LocalizationKeys, string> EnglishDict = new Dictionary<LocalizationKeys, string>();

        public LocalizationManager()
        {
            EnglishDict.Add(LocalizationKeys.KillEnemies, "Kill required amount of enemies : {0} / {1}");
            EnglishDict.Add(LocalizationKeys.ReachDestination, "Reach helicopter landing site!");
            EnglishDict.Add(LocalizationKeys.SurviveTime, "Survive until timer goes to zero : {0:f0}");
        }

        public string GetText(LocalizationKeys key)
        {
            return EnglishDict[key];
        }
    }
}