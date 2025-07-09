namespace Game.Features.Composite
{
    public class Program
    {
        private const string _objectActivate = "portal";
        private const string _collectItemType = "golden_key";
        private readonly WinConditionController _winController;
        private readonly GameState _gameState;

        public Program()
        {
            _winController = new WinConditionController();
            _gameState = new GameState();
            SetupLevel();
            RunExample();
        }

        private void SetupLevel()
        {
            // example: "(break all ice OR collect golden key) AND activate portal"
            var condition = WinConditionBuilder.And(
                WinConditionBuilder.Or(
                    WinConditionBuilder.BreakAllTiles("ice"),
                    WinConditionBuilder.CollectItem(_collectItemType, 1)
                ),
                WinConditionBuilder.ActivateObject(_objectActivate)
            );

            _winController.SetWinCondition(condition);
        }

        public void RunExample()
        {
            // part 1
            _gameState.ItemCounts[_collectItemType] = 1;  // player found key
            _gameState.ActivatedObjects.Add(_objectActivate); // player activated portal

            bool hasWon = _winController.CheckWinCondition(_gameState);
            // hasWon = true because key collected AND portal activated

            // part 2
            _winController.ClearWinCondition();
            bool hasWonAfterClear = _winController.CheckWinCondition(_gameState);
            // hasWonAfterClear = false because no conditions set
        }

        public string GetCurrentObjective()
        {
            return _winController.GetConditionDescription();
        }
    }
}
