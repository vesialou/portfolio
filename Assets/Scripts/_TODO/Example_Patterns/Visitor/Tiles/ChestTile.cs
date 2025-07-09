namespace Game.Features.Visitor
{
    public class ChestTile : BaseTile
    {
        private readonly string _rewardType;
        private bool _isOpened;

        public string RewardType => _rewardType;
        public bool IsOpened => _isOpened;

        public ChestTile(TileModel model, string rewardType) : base(model)
        {
            _rewardType = rewardType;
            _isOpened = false;
        }

        public void Open()
        {
            _isOpened = true;
        }

        public override void Accept(ITileVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}