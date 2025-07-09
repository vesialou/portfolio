namespace Game.Features.Strategy
{
    public class Program
    {
        private readonly PlayerBehaviorTracker _behaviorTracker;
        private readonly OfferManager _offerManager;

        public Program()
        {
            _behaviorTracker = new PlayerBehaviorTracker();
            _offerManager = new OfferManager(_behaviorTracker);

            PlayerBehaviorMock.SimulateTypicalFrustration(_behaviorTracker);
            LevelComplete();
        }

        public void LevelComplete()
        {
            //level complete! selecting offer
            _offerManager.ShowLevelCompleteOffer();
        }
    }
}
