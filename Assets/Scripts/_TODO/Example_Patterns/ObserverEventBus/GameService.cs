using System;

namespace Game.Features.Observer
{
    public class GameService : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly ScoreModel _scoreModel;
        private readonly QuestModel _questModel;
        private readonly AnalyticsModel _analyticsModel;


        public GameService(IEventBus eventBus, ScoreModel scoreModel, QuestModel questModel, AnalyticsModel analyticsModel)
        {
            _eventBus = eventBus;
            _scoreModel = scoreModel;
            _questModel = questModel;
            _analyticsModel = analyticsModel;

            InitializeControllers();
        }

        private void InitializeControllers()
        {
            var scoreController = new ScoreController(_scoreModel, _eventBus);
            var questController = new QuestController(_questModel, _eventBus);
            var analyticsController = new AnalyticsController(_analyticsModel);

            // Subscribe with different priorities
            _eventBus.Subscribe(typeof(SpecialGemMatchedEvent), analyticsController, EventPriority.Critical);
            _eventBus.Subscribe(typeof(ScoreChangedEvent), analyticsController, EventPriority.Critical);
            _eventBus.Subscribe(typeof(QuestProgressEvent), analyticsController, EventPriority.Critical);

            _eventBus.Subscribe(typeof(SpecialGemMatchedEvent), scoreController, EventPriority.High);
            _eventBus.Subscribe(typeof(SpecialGemMatchedEvent), questController, EventPriority.High);

        }

        public void ProcessSpecialGemMatch(GemType gemType, int comboMultiplier)
        {
            var specialGemEvent = new SpecialGemMatchedEvent(gemType, comboMultiplier);
            _eventBus.Publish(specialGemEvent);
        }

        public void Dispose()
        {
            _eventBus.Clear();
        }
    }
}