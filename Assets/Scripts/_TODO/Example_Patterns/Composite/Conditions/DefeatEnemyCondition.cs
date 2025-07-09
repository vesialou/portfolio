namespace Game.Features.Composite
{
    public class DefeatEnemyCondition : IWinCondition
    {
        private readonly string _enemyType;

        public DefeatEnemyCondition(string enemyType)
        {
            this._enemyType = enemyType;
        }

        public bool Evaluate(GameState state)
        {
            return state.DefeatedEnemies.Contains(_enemyType);
        }

        public string Description
        {
            get => $"defeat {_enemyType}";
        }
    }
}