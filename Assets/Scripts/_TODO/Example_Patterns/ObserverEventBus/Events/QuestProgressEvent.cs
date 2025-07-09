using System;

namespace Game.Features.Observer
{
    public class QuestProgressEvent : IGameEvent
    {
        public string QuestId { get; }
        public QuestType QuestType { get; }
        public int Progress { get; }
        public int Target { get; }
        public bool IsCompleted { get; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;

        public QuestProgressEvent(string questId, QuestType questType, int progress, int target)
        {
            QuestId = questId;
            QuestType = questType;
            Progress = progress;
            Target = target;
            IsCompleted = progress >= target;
        }
    }
}