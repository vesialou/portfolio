using System.Collections.Generic;

namespace Game.Features.CommandMemento
{
    public class ModifyWorkersCommand : IUndoableCommand
    {
        private readonly ComponentBuilding _building;
        private readonly int _newCount;
        private readonly float _newEfficiency;
        private readonly Stack<ComponentBuildingMemento> _mementoStack = new();

        public string Label { get; }

        public ModifyWorkersCommand(ComponentBuilding building, int newCount, float newEfficiency, string label)
        {
            _building = building;
            _newCount = newCount;
            _newEfficiency = newEfficiency;
            Label = label;
        }

        public bool CanExecute()
        {
            bool buildingExists = _building != null && _newCount >= 0;
            return buildingExists && _newEfficiency > 0;
        }

        public void Execute()
        {
            if (!CanExecute())
            {
                return;
            }

            var memento = _building.CreateMemento();
            _mementoStack.Push(memento);

            var workerComponent = _building.GetWorkerComponent();
            if (workerComponent != null)
            {
                workerComponent.Count = _newCount;
                workerComponent.Efficiency = _newEfficiency;
            }
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