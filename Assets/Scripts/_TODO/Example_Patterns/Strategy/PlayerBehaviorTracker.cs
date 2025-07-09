namespace Game.Features.Strategy
{
    public class PlayerBehaviorTracker
    {
        private int skipCount = 0;
        private int restartCount = 0;
        private float idleTime = 0f;
        private bool usedBoost = false;
        private bool lostWithBoost = false;

        public static readonly PlayerTrackerConfig _config = new PlayerTrackerConfig( skipCountLimit: 3, restartCountLimit: 2, idleTimeLimit: 5f );


        public void OnSkip() => skipCount++;
        public void OnRestart() => restartCount++;
        public void OnIdleBeforeLose(float time) => idleTime = time;
        public void OnBoostUsed(bool isActive) => usedBoost = isActive;
        public void OnGameLost() => lostWithBoost = usedBoost;

        public PlayerBehaviorFlags Evaluate()
        {
            return new PlayerBehaviorFlags(
                skipsALot: skipCount >= _config.SkipCountLimit,
                restartsALot: restartCount >= _config.RestartCountLimit,
                wasIdle: idleTime > _config.IdleTimeLimit,
                failedWithBoost: lostWithBoost
            );
        }

        public void Reset()
        {
            skipCount = 0;
            restartCount = 0;
            idleTime = 0f;
            usedBoost = false;
            lostWithBoost = false;
        }
    }

    public struct PlayerBehaviorFlags
    {
        public readonly bool SkipsALot;
        public readonly bool RestartsALot;
        public readonly bool WasIdle;
        public readonly bool FailedWithBoost;

        public PlayerBehaviorFlags(bool skipsALot, bool restartsALot, bool wasIdle, bool failedWithBoost) : this()
        {
            SkipsALot = skipsALot;
            RestartsALot = restartsALot;
            WasIdle = wasIdle;
            FailedWithBoost = failedWithBoost;
        }
    }

    public struct PlayerTrackerConfig
    {
        public readonly int SkipCountLimit;
        public readonly int RestartCountLimit;
        public readonly float IdleTimeLimit;

        public PlayerTrackerConfig(int skipCountLimit, int restartCountLimit, float idleTimeLimit)
        {
            SkipCountLimit = skipCountLimit;
            RestartCountLimit = restartCountLimit;
            IdleTimeLimit = idleTimeLimit;
        }
    }
}
