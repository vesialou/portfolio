using System.Collections.Generic;

namespace Game.Features.CommandMemento
{
    public class CommandManager
    {
        private readonly Stack<IUndoableCommand> _commandHistory = new();
        private readonly Queue<IUndoableCommand> _pendingCommands = new();
        private readonly int _maxHistorySize;

        public int HistoryCount => _commandHistory.Count;
        public int PendingCount => _pendingCommands.Count;

        public CommandManager(int maxHistorySize = 100)
        {
            _maxHistorySize = maxHistorySize;
        }

        public void ExecuteCommand(IUndoableCommand command)
        {
            if (!command.CanExecute())
            {
                return;
            }

            command.Execute();
            _commandHistory.Push(command);
            TrimHistoryIfNeeded();
        }

        public bool QueueCommand(IUndoableCommand command)
        {
            if (command.CanExecute())
            {
                _pendingCommands.Enqueue(command);
                return true;
            }

            return false;
        }

        public bool ProcessNextQueuedCommand()
        {
            if (_pendingCommands.Count == 0)
            {
                return false;
            }

            var command = _pendingCommands.Dequeue();
            if (command.CanExecute())
            {
                ExecuteCommand(command);
                return true;
            }

            return false;
        }

        public void UndoLastCommand()
        {
            if (_commandHistory.Count == 0)
            {
                return;
            }

            var command = _commandHistory.Pop();
            command.Undo();
        }

        public void UndoMultiple(int count)
        {
            for (var i = 0; i < count && _commandHistory.Count > 0; i++)
            {
                UndoLastCommand();
            }
        }

        public void ClearPendingCommands() => _pendingCommands.Clear();
        public void ClearHistory() => _commandHistory.Clear();

        private void TrimHistoryIfNeeded()
        {
            if (_commandHistory.Count > _maxHistorySize)
            {
                // TODO: limit undo history here if needed
            }
        }

        public IEnumerable<string> GetHistoryLabels()
        {
            foreach (var command in _commandHistory)
            {
                yield return command.Label;
            }
        }
    }
}