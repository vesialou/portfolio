namespace Game.Features.CommandMemento
{
    public class WorkerComponent : IBuildingComponent<WorkerSnapshot>
    {
        public string ComponentType => "Workers";
        public int Count { get; set; } = 0;
        public float Efficiency { get; set; } = 1.0f;

        public WorkerSnapshot CreateSnapshot()
        {
            return new(Count, Efficiency);
        }

        public void RestoreFromSnapshot(WorkerSnapshot snapshot)
        {
            Count = snapshot.Count;
            Efficiency = snapshot.Efficiency;
        }
    }
}