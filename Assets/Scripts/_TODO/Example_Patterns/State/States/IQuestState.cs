namespace Game.Features.State
{
    public interface IQuestState
    {
        void Enter(QuestStateMachine stateMachine);
        void Exit(QuestStateMachine stateMachine);
        void HandleClick(QuestStateMachine stateMachine);
        void UpdateVisuals(QuestStateMachine stateMachine);
    }
}