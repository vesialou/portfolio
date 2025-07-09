namespace Game.Features.State
{
    public class ClaimedQuestState : IQuestState
    {
        public void Enter(QuestStateMachine stateMachine)
        {
            // reward is now CLAIMED
        }

        public void Exit(QuestStateMachine stateMachine)
        {
            // exiting CLAIMED state
        }

        public void HandleClick(QuestStateMachine stateMachine)
        {
            //Quest already claimed - showing completion info
        }

        public void UpdateVisuals(QuestStateMachine stateMachine)
        {
            // Updated visuals for CLAIMED quest
        }
    }
}