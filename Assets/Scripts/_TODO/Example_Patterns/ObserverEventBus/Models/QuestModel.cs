using System.Collections.Generic;

namespace Game.Features.Observer
{
    public class QuestModel
    {
        private readonly List<QuestData> _activeQuests = new();

        public IReadOnlyList<QuestData> GetActiveQuests() => _activeQuests;

        public void AddQuest(QuestData quest)
        {
            _activeQuests.Add(quest);
        }

        public void RemoveQuest(string questId)
        {
            _activeQuests.RemoveAll(q => q.Id == questId);
        }
    }
}