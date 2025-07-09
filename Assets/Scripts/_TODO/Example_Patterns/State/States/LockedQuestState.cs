namespace Game.Features.State
{
    public class LockedQuestState : IQuestState
    {
        public void Enter(QuestStateMachine stateMachine)
        {
            // is now LOCKED hide progress, disable interactions
        }

        public void Exit(QuestStateMachine stateMachine)
        {
            // exiting LOCKED state cleanup locked state visuals
        }

        public void HandleClick(QuestStateMachine stateMachine)
        {
            // Cannot interact with locked quest
        }

        public void UpdateVisuals(QuestStateMachine stateMachine)
        {
            // Updated visuals for LOCKED quest
        }
    }
}