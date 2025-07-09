namespace Game.Features.Strategy
{
    public static class PlayerBehaviorMock
    {
        public static void SimulateTypicalFrustration(PlayerBehaviorTracker tracker)
        {
            tracker.OnSkip();
            tracker.OnSkip();
            tracker.OnRestart();
            tracker.OnBoostUsed(true);
            tracker.OnGameLost();
        }
    }
}
