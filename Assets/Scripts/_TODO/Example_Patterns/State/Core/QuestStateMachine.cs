using System;

namespace Game.Features.State
{
    public class QuestStateMachine
    {
        public event Action OnStateChanged;

        private IQuestState _currentState;

        private readonly QuestData _questData;

        private readonly LockedQuestState _lockedState;
        private readonly ActiveQuestState _activeState;
        private readonly CompletedQuestState _completedState;
        private readonly ClaimedQuestState _claimedState;


        public QuestStateMachine(QuestData data)
        {
            _questData = data;

            _lockedState = new LockedQuestState();
            _activeState = new ActiveQuestState();
            _completedState = new CompletedQuestState();
            _claimedState = new ClaimedQuestState();

            ChangeState(_lockedState);
        }

        public LockedQuestState GetLockedState() => _lockedState;
        public ActiveQuestState GetActiveState() => _activeState;
        public CompletedQuestState GetCompletedState() => _completedState;
        public ClaimedQuestState GetClaimedState() => _claimedState;

        public void HandleClick()
        {
            _currentState?.HandleClick(this);
        }

        public QuestData GetQuestData()
        {
            return _questData;
        }

        public void Unlock()
        {
            if (_currentState is LockedQuestState)
            {
                ChangeState(GetActiveState());
            }
        }

        public void Complete()
        {
            if (_currentState is ActiveQuestState)
            {
                ChangeState(GetCompletedState());
            }
        }

        public void Claim()
        {
            if (_currentState is CompletedQuestState)
            {
                ChangeState(GetClaimedState());
            }
        }

        public void ResetToLocked()
        {
            ChangeState(GetLockedState());
        }

        private void ChangeState(IQuestState newState)
        {
            if (newState == null || newState == _currentState)
            {
                return;
            }

            _currentState?.Exit(this);

            _currentState = newState;
            _currentState.Enter(this);

            _currentState.UpdateVisuals(this);

            OnStateChanged?.Invoke();
        }
    }
}