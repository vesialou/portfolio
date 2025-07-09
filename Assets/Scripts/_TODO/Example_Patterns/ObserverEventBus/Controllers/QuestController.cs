namespace Game.Features.Observer
{
    public class QuestController : IEventHandler
    {
        private readonly QuestModel _questModel;
        private readonly IEventBus _eventBus;

        public QuestController(QuestModel questModel, IEventBus eventBus)
        {
            _questModel = questModel;
            _eventBus = eventBus;
        }

        public void Handle(IGameEvent e)
        {
            if (e is SpecialGemMatchedEvent)
            {
                var activeQuests = _questModel.GetActiveQuests();

                for (var i = 0; i < activeQuests.Count; i++)
                {
                    QuestData quest = activeQuests[i];
                    if (quest.QuestType == QuestType.MatchSpecialGems)
                    {
                        quest.IncrementProgress();
                        _eventBus.Publish(new QuestProgressEvent(quest.Id, quest.QuestType, quest.Progress, quest.Target));
                    }
                }
            }

        }
    }
}