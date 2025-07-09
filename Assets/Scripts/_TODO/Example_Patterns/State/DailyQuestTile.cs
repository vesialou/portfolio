using System;

namespace Game.Features.State
{
    public class DailyQuestTile : IDisposable
    {
        private readonly QuestStateMachine _stateMachine;
        private readonly QuestData _questData;

        public DailyQuestTile(QuestData data)
        {
            _questData = data;
            _stateMachine = new QuestStateMachine(_questData);

            _stateMachine.OnStateChanged += OnQuestStateChanged;
        }

        public void OnTileClicked()
        {
            _stateMachine.HandleClick();
        }

        public void UpdateQuestProgress(int newProgress)
        {
            // updating quest auto-complete if progress reaches target
            if (newProgress >= 100)
            {
                _stateMachine.Complete();
            }
        }

        public void UnlockQuest()
        {
            _stateMachine.Unlock();
        }

        private void OnQuestStateChanged()
        {
            //Quest tile state changed
        }

        public void Dispose()
        {
            _stateMachine.OnStateChanged -= OnQuestStateChanged;
        }
    }
}