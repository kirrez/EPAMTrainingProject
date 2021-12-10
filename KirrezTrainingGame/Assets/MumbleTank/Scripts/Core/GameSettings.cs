using System;

namespace TankGame
{
    public class GameSettings : IGameSettings
    {
        public Levels CurrentLevel { get; set; }

        public float GetStartPositionY(EnemyType type)
        {
            float newY = 0f;
            switch (type)
            {
                case EnemyType.Spikedmine:
                    newY = 10f;
                    break;
                case EnemyType.Turret:
                    newY = -4f;
                    break;
                case EnemyType.Flyer:
                    newY = 2f;
                    break;
            }
            return newY;
        }
    }
}