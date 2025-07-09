using System.Collections.Generic;

namespace Game.Features.CommandMemento
{
    public class ModifyProductionCommand : IUndoableCommand
    {
        private readonly ComponentBuilding _building;
        private readonly float _newRate;
        private readonly Stack<ComponentBuildingMemento> _mementoStack = new();

        public string Label { get; }

        public ModifyProductionCommand(ComponentBuilding building, float newRate, string label)
        {
            _building = building;
            _newRate = newRate;
            Label = label;
        }

        public bool CanExecute()
        {
            return _building != null && _newRate > 0;
        }

        public void Execute()
        {
            if (!CanExecute())
            {
                return;
            }

            var memento = _building.CreateMemento();
            _mementoStack.Push(memento);

            var productionComponent = _building.GetProductionComponent();
            if (productionComponent != null)
            {
                productionComponent.Rate = _newRate;
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