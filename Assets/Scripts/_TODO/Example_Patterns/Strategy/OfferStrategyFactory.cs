namespace Game.Features.Strategy
{
    public static class OfferStrategyFactory
    {
        public static IOfferStrategy Create(PlayerBehaviorFlags flags)
        {
            if (flags.FailedWithBoost)
            {
                return new BoostRefundOfferStrategy();
            }

            if (flags.SkipsALot)
            {
                return new RetryOfferStrategy();
            }

            if (flags.RestartsALot)
            {
                return new HelpPackOfferStrategy();
            }

            if (flags.WasIdle)
            {
                return new SoftMotivationOfferStrategy();
            }

            return new RetryOfferStrategy(); // fallback
        }
    }
}
