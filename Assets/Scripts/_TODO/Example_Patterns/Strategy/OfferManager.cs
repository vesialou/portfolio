namespace Game.Features.Strategy
{
    public class OfferManager
    {
        private readonly PlayerBehaviorTracker _behaviorTracker;

        public OfferManager(PlayerBehaviorTracker behaviorTracker)
        {
            _behaviorTracker = behaviorTracker;
        }

        public void ShowLevelCompleteOffer()
        {
            var flags = _behaviorTracker.Evaluate();
            var strategy = OfferStrategyFactory.Create(flags);
            strategy.ShowOffer();
            _behaviorTracker.Reset();
        }
    }
}
