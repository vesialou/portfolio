using System;

namespace Game.Features.Observer
{
    public class QuestData
    {
        public string Id { get; }
        public QuestType QuestType { get; }
        public int Progress { get; private set; }
        public int Target { get; }

        public QuestData(string id, QuestType questType, int target)
        {
            Id = id;
            QuestType = questType;
            Target = target;
            Progress = 0;
        }

        public void IncrementProgress(int amount = 1)
        {
            Progress = Math.Min(Progress + amount, Target);
        }
    }
}