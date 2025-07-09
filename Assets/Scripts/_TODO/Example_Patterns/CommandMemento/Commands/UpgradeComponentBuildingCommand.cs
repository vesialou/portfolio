using System.Collections.Generic;

namespace Game.Features.CommandMemento
{
    public class UpgradeComponentBuildingCommand : IUndoableCommand
    {
        public string Label { get; }

        private readonly ComponentBuilding _building;
        private readonly Stack<ComponentBuildingMemento> _mementoStack = new();


        public UpgradeComponentBuildingCommand(ComponentBuilding building, string label)
        {
            _building = building;
            Label = label;
        }

        public bool CanExecute() => _building != null;

        public void Execute()
        {
            if (!CanExecute())
            {
                return;
            }

            var memento = _building.CreateMemento();
            _mementoStack.Push(memento);
            _building.ProcessUpgrade();
        }

        public void Undo()
        {
            if (_mementoStack.Count == 0)
            {
                return;
            }

            var memento = _mementoStack.Pop();
            _building.RestoreFromMemento(memento);
        }
    }
}