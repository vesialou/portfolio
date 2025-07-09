namespace Game.Features.State
{
    public class ActiveQuestState : IQuestState
    {
        public void Enter(QuestStateMachine stateMachine)
        {
            // is now ACTIVE progress tracking
        }

        public void Exit(QuestStateMachine stateMachine)
        {
            // exiting ACTIVE state stop progress tracking, save current progress
        }

        public void HandleClick(QuestStateMachine stateMachine)
        {
            // Clicked on active quest - showing details or quest can be completed
            if (CanCompleteQuest(stateMachine.GetQuestData()))
            {
                stateMachine.Complete();
            }
        }

        public void UpdateVisuals(QuestStateMachine stateMachine)
        {
            // Updated visuals for ACTIVE quest
        }

        private bool CanCompleteQuest(QuestData questData)
        {
            // implement actual completion check
            return false;
        }
    }
}