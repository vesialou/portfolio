using System.Collections.Generic;

namespace Game.Features.Composite
{
    public class CollectItemCondition : IWinCondition
    {
        private readonly string _itemType;
        private readonly int _requiredCount;

        public CollectItemCondition(string itemType, int requiredCount)
        {
            _itemType = itemType;
            _requiredCount = requiredCount;
        }

        public bool Evaluate(GameState state)
        {
            var count = state.ItemCounts.GetValueOrDefault(_itemType, 0);
            return count >= _requiredCount;
        }

        public string Description
        {
            get => $"collect {_requiredCount} {_itemType}";
        }
    }
}