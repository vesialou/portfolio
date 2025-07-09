using System;

namespace Game.Features.Observer
{
    public class ScoreChangedEvent : IGameEvent
    {
        public int PreviousScore { get; }
        public int NewScore { get; }
        public int Delta { get; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public ScoreChangedEvent(int previousScore, int newScore)
        {
            PreviousScore = previousScore;
            NewScore = newScore;
            Delta = newScore - previousScore;
        }
    }
}