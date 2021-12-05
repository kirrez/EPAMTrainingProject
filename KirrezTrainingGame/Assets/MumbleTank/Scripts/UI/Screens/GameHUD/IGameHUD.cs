public interface IGameHUD : IScreen
{
    void SetTaskDescription(string description);
    void SetTaskDescription(string description, float value);
    void SetTaskDescription(string description, int current, int maximum);
}