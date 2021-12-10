
namespace TankGame
{
    public class PlayerSettings : IPlayerSettings
    {
        public int MaxHealth { get { return 7; } }
        public int MinHealth { get { return 1; } }

        public int StartHealth { get; set; } = 3;
        public float ShieldTime { get { return 1.5f; } }
    }
}