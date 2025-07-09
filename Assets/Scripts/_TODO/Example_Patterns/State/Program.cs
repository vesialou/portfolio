namespace Game.Features.State
{
    public class Program
    {
        public static void WorflowQuest()
        {
            var questData = new QuestData(
                "daily_001",
                "Collect 10 items",
                "Find and collect"
            );

            var questTile = new DailyQuestTile(questData);

            // 1. Initial state (Locked)
            questTile.OnTileClicked();

            // 2. Unlocking quest
            questTile.UnlockQuest();
            questTile.OnTileClicked();

            // 3. Updating progress
            questTile.UpdateQuestProgress(50);
            questTile.OnTileClicked();

            // 4. Completing quest
            questTile.UpdateQuestProgress(100);
            questTile.OnTileClicked();

            // 5. Quest completed and claimed
            questTile.OnTileClicked();

            questTile.Dispose();
        }
    }
}