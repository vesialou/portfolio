namespace Game.Features.State
{
    public class CompletedQuestState : IQuestState
    {
        public void Enter(QuestStateMachine stateMachine)
        {
            // is now COMPLETED show reward button
        }

        public void Exit(QuestStateMachine stateMachine)
        {
            // exiting COMPLETED state hide completion effects
        }

        public void HandleClick(QuestStateMachine stateMachine)
        {
            // Claiming quest rewards to player
            GiveReward(stateMachine.GetQuestData());
            stateMachine.Claim();
        }

        public void UpdateVisuals(QuestStateMachine stateMachine)
        {
            // Updated visuals for COMPLETED quest
        }

        private void GiveReward(QuestData questData)
        {
            // Giving reward for questData
        }
    }
}