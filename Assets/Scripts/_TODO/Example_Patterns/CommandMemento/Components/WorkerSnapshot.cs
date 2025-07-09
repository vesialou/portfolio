namespace Game.Features.CommandMemento
{
    public class WorkerSnapshot
    {
        public int Count { get; }
        public float Efficiency { get; }
        public WorkerSnapshot(int count, float efficiency)
        {
            Count = count;
            Efficiency = efficiency;
        }
    }
}