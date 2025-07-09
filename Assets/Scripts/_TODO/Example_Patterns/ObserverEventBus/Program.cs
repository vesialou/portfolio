namespace Game.Features.Observer
{
    public class Program
    {
        public void SimulateGameplay()
        {
            var gameService = new GameService(
                new EventBus(),
                new ScoreModel(),
                new QuestModel(),
                new AnalyticsModel());

            gameService.ProcessSpecialGemMatch(GemType.Bomb, 2);

            gameService.Dispose();
        }
    }
}