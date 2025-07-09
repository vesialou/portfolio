using Unity.VisualScripting;

namespace Game.Features.CommandMemento
{
    public class Program
    {
        private CommandManager _commandManager;
        private ComponentBuilding _factory;
        private ComponentBuilding _powerPlant;

        public Program()
        {
            _commandManager = new CommandManager(maxHistorySize: 50);
            _factory = new ComponentBuilding("Factory");
            _powerPlant = new ComponentBuilding("Power Plant");

            SetupComponents();
            ExecuteCommands();
            TestUndo();

            TestBatchOperations();
            TestMementoPattern();
        }

        private void SetupComponents()
        {
            _factory.AddComponent(new ProductionComponent { Rate = 1.0f });
            _factory.AddComponent(new WorkerComponent { Count = 10, Efficiency = 1.0f });

            _powerPlant.AddComponent(new ProductionComponent { Rate = 2.0f });
            _powerPlant.AddComponent(new WorkerComponent { Count = 5, Efficiency = 1.5f });
        }

        private void ExecuteCommands()
        {
            _commandManager.ExecuteCommand
                (new UpgradeComponentBuildingCommand(_factory, "Factory Upgrade"));
            _commandManager.ExecuteCommand(
                new ModifyProductionCommand(_factory, 2.5f, "Increase Production"));
            _commandManager.ExecuteCommand(
                new ModifyWorkersCommand(_factory, 15, 1.2f, "Hire More Workers"));
        }

        private void TestUndo()
        {
            _commandManager.UndoLastCommand();
            _commandManager.UndoLastCommand();
        }

        private void TestBatchOperations()
        {
            var result = _commandManager.QueueCommand(new ModifyProductionCommand(_powerPlant, 3.0f, "Power Plant Boost"));
            result &= _commandManager.QueueCommand(new ModifyWorkersCommand(_powerPlant, 8, 1.8f, "Power Plant Staff"));

            while (result && _commandManager.PendingCount > 0)
            {
                _commandManager.ProcessNextQueuedCommand();
            }
        }

        private void TestMementoPattern()
        {
            var memento = _factory.CreateMemento();

            var modifyCommand = new ModifyProductionCommand(_factory, 5.0f, "Test Modification");
            _commandManager.ExecuteCommand(modifyCommand);

            _factory.RestoreFromMemento(memento);
        }
    }
}