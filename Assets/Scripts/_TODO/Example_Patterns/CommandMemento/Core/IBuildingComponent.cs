namespace Game.Features.CommandMemento
{
    public interface IBuildingComponent<TSnapshot>
    {
        string ComponentType { get; }
        TSnapshot CreateSnapshot();
        void RestoreFromSnapshot(TSnapshot snapshot);
    }
}