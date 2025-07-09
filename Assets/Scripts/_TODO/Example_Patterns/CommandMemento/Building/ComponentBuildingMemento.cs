namespace Game.Features.CommandMemento
{
    public class ComponentBuildingMemento
    {
        public BuildingData Data { get; }
        public ProductionSnapshot ProductionSnapshot { get; }
        public WorkerSnapshot WorkerSnapshot { get; }

        public ComponentBuildingMemento(BuildingData data, ProductionSnapshot prod, WorkerSnapshot worker)
        {
            Data = data;
            ProductionSnapshot = prod;
            WorkerSnapshot = worker;
        }
    }
}