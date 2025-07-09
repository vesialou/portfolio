namespace Game.Features.Composite
{
    public class ActivateObjectCondition : IWinCondition
    {
        private readonly string _objectType;

        public ActivateObjectCondition(string objectType)
        {
            _objectType = objectType;
        }

        public bool Evaluate(GameState state)
        {
            return state.ActivatedObjects.Contains(_objectType);
        }

        public string Description
        {
            get => $"activate {_objectType}";
        }
    }
}