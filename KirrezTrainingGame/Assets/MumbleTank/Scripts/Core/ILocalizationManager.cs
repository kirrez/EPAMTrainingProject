using UnityEngine;

namespace TankGame
{
    public interface ILocalizationManager
    {
        string GetText(LocalizationKeys key);
    }
}