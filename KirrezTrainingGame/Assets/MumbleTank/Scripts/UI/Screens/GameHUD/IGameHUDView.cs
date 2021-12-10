
namespace TankGame
{
    public interface IGameHUDView : IView
    {
        void SetHealth(int value);
        void SetMaxHealth(int value);

        void SetShieldActive(bool isActive);
        void SetShieldProgress(float normalizedValue);

        void SetTaskDescription(string description);
        void SetTaskDescription(string description, float value);
        void SetTaskDescription(string description, int current, int maximum);
    }
}