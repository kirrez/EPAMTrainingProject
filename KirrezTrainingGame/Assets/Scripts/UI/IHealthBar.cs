public interface IHealthBar : IView
{
    void ResetHealth();

    void SetMaxHealth(float value);
    void SetHealth(float value);
}
