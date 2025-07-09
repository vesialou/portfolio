namespace Game.Features.State
{
    public static class QuestStateExtensions
    {
        public static bool IsLocked(this IQuestState state) => state is LockedQuestState;
        public static bool IsActive(this IQuestState state) => state is ActiveQuestState;
        public static bool IsCompleted(this IQuestState state) => state is CompletedQuestState;
        public static bool IsClaimed(this IQuestState state) => state is ClaimedQuestState;
    }
}