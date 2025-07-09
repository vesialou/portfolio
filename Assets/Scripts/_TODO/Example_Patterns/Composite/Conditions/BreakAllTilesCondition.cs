namespace Game.Features.Composite
{
    public class BreakAllTilesCondition : IWinCondition
    {
        private readonly string _tileType;

        public BreakAllTilesCondition(string tileType)
        {
            _tileType = tileType;
        }

        public bool Evaluate(GameState state)
        {
            return state.BrokenTiles.Count >= state.TotalTiles;
        }

        public string Description
        {
            get => $"break all {_tileType} tiles";
        }
    }
}