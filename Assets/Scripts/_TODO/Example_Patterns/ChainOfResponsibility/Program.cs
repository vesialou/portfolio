namespace Game.Features.ChainOfResponsibility
{
    public struct LevelData {}

    public class Program
    {
        public void StartLevel(int levelId)
        {
            var level = new LevelData();
            LevelAdaptationChain.ProcessLevel(ref level);

            // Level is now adapted and ready to use
        }
    }
}