using System.Collections.Generic;

namespace Game.Features.ChainOfResponsibility
{
    public static class LevelAdaptationChain
    {
        private static LevelAdaptationHandler _chainRoot;

        public static void ProcessLevel(ref LevelData context)
        {
            _chainRoot ??= BuildChain();
            _chainRoot.Handle(ref context);
        }

        private static LevelAdaptationHandler BuildChain()
        {
            var handlers = new List<LevelAdaptationHandler>
            {
                new ValidationHandler(),
                new FTUEHandler(),
                new DifficultyHandler(),
                new MonetizationHandler()
            };

            for (var i = 0; i < handlers.Count - 1; i++)
            {
                handlers[i].SetNext(handlers[i + 1]);
            }

            return handlers[0];
        }
    }
}
