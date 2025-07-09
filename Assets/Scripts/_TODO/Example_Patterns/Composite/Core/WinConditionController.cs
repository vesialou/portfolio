namespace Game.Features.Composite
{
    public class WinConditionController
    {
        private IWinCondition rootCondition;

        public void SetWinCondition(IWinCondition condition)
        {
            rootCondition = condition;
        }

        public void ClearWinCondition()
        {
            rootCondition = null;
        }

        public bool CheckWinCondition(GameState state)
        {
            if (rootCondition == null)
            {
                return false;
            }

            return rootCondition.Evaluate(state);
        }

        public string GetConditionDescription()
        {
            if (rootCondition == null)
            {
                return "no condition set";
            }

            return rootCondition.Description;
        }
    }
}