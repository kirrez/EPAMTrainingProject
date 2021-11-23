public interface IGameHUDView : IView
{
    void SetHealth(int value);
    void SetMaxHealth(int value);

    void SetShieldActive(bool isActive);
    void SetShieldProgress(float normalizedValue);
}