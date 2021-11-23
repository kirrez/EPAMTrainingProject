public interface IPlayerSettings
{
    int MaxHealth { get; }
    int MinHealth { get; }

    int StartHealth { get; set; }
    float ShieldTime { get; }
}