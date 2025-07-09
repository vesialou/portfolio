namespace Game.Features.Composite
{
    public class WinConditionBuilder
    {
        public static CollectItemCondition CollectItem(string itemType, int count)
        {
            return new CollectItemCondition(itemType, count);
        }

        public static DefeatEnemyCondition DefeatEnemy(string enemyType)
        {
            return new DefeatEnemyCondition(enemyType);
        }

        public static ActivateObjectCondition ActivateObject(string objectType)
        {
            return new ActivateObjectCondition(objectType);
        }

        public static BreakAllTilesCondition BreakAllTiles(string tileType)
        {
            return new BreakAllTilesCondition(tileType);
        }

        public static AndCondition And(params IWinCondition[] conditions)
        {
            return new AndCondition(conditions);
        }

        public static OrCondition Or(params IWinCondition[] conditions)
        {
            return new OrCondition(conditions);
        }
    }
}