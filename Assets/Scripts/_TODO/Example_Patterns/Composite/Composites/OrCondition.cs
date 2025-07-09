using System.Collections.Generic;

namespace Game.Features.Composite
{
    public class OrCondition : IWinCondition
    {
        private readonly List<IWinCondition> _conditions;

        public OrCondition(params IWinCondition[] conditions)
        {
            _conditions = new List<IWinCondition>(conditions);
        }

        public void AddCondition(IWinCondition condition)
        {
            _conditions.Add(condition);
        }

        public bool Evaluate(GameState state)
        {
            var result = false;
            for (var i = 0; i < _conditions.Count; i++)
            {
                if (_conditions[i].Evaluate(state))
                {
                    result = true;
                    break;

                }
            }

            return result;
        }

        public string Description
        {
            get => $"({string.Join(" OR ", GetDescriptions())})";
        }

        private string[] GetDescriptions()
        {
            var descriptions = new string[_conditions.Count];
            for (int i = 0; i < _conditions.Count; i++)
            {
                descriptions[i] = _conditions[i].Description;
            }
            return descriptions;
        }
    }
}