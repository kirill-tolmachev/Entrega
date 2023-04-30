using Scripts.Infrastructure.Messages;

namespace Assets.Scripts.MessageImpl
{
    internal class TotalScoreChangedMessage : IMessage
    {
        public TotalScoreChangedMessage(int localScore, int totalScore)
        {
            LocalScore = localScore;
            TotalScore = totalScore;
        }

        public int LocalScore { get; }
        public int TotalScore { get; }

    }
}