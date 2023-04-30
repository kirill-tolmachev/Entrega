using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts.Infrastructure.Messages;

namespace Assets.Scripts.MessageImpl
{
    internal class LevelShouldChangeMessage : IMessage
    {
        public LevelShouldChangeMessage(int levelIndex, LevelInfo level, int requiredScore, float enemyFrequency)
        {
            LevelIndex = levelIndex;
            Level = level;
            RequiredScore = requiredScore;
            EnemyFrequency = enemyFrequency;
        }

        public int LevelIndex { get; }
        public LevelInfo Level { get; }

        public int RequiredScore { get; }
        public float EnemyFrequency { get; }
    }
}
