namespace Game.Features.CommandMemento
{
    public class ComponentBuilding
    {
        public string Name { get; }
        public BuildingData Data { get; private set; }

        private ProductionComponent _production;
        private WorkerComponent _workers;

        public ComponentBuilding(string name)
        {
            Name = name;
            Data = new BuildingData();
        }

        public void AddComponent(ProductionComponent component) => _production = component;
        public void AddComponent(WorkerComponent component) => _workers = component;

        public ProductionComponent GetProductionComponent() => _production;
        public WorkerComponent GetWorkerComponent() => _workers;

        public ComponentBuildingMemento CreateMemento()
        {
            return new ComponentBuildingMemento(
                Data,
                _production?.CreateSnapshot(),
                _workers?.CreateSnapshot()
            );
        }

        public void RestoreFromMemento(ComponentBuildingMemento memento)
        {
            Data = memento.Data;

            _production?.RestoreFromSnapshot(memento.ProductionSnapshot);
            _workers?.RestoreFromSnapshot(memento.WorkerSnapshot);
        }

        public void ProcessUpgrade() { }
    }
}