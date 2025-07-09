namespace Game.Features.Observer
{
    public class ScoreController : IEventHandler
    {
        private readonly ScoreModel _scoreModel;
        private readonly IEventBus _eventBus;

        public ScoreController(ScoreModel scoreModel, IEventBus eventBus)
        {
            _scoreModel = scoreModel;
            _eventBus = eventBus;
        }

        public void Handle(IGameEvent e)
        {
            if (e is SpecialGemMatchedEvent sGameEvent)
            {
                var previousScore = _scoreModel.CurrentScore;
                var basePoints = GetBasePoints(sGameEvent.GemType);

                _scoreModel.AddScore(basePoints + 1);

                _eventBus.Publish(new ScoreChangedEvent(previousScore, _scoreModel.CurrentScore));
            }
        }

        private static int GetBasePoints(GemType gemType)
        {
            return gemType switch
            {
                GemType.Bomb => 5,
                _ => 1
            };
        }
    }
}