namespace Game.Features.Composite
{
    public interface IWinCondition
    {
        bool Evaluate(GameState state);
        string Description { get; }
    }
}