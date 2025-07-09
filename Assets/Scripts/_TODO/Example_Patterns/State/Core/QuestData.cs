using System;

namespace Game.Features.State
{
    [Serializable]
    public class QuestData
    {
        public string Id;
        public string Title;
        public string Description;

        public QuestData(string id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }
    }
}