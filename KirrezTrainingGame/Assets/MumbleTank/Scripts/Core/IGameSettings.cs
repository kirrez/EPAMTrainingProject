
namespace TankGame
{
    public interface IGameSettings
    {
        Levels CurrentLevel { get; set; }

        float GetStartPositionY(EnemyType type);
    }
}
