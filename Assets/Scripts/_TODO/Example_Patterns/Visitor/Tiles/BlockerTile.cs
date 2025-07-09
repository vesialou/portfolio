namespace Game.Features.Visitor
{
    public class BlockerTile : BaseTile
    {
        private readonly bool _isDestructible;
        public bool IsDestructible => _isDestructible;

        public BlockerTile(TileModel model, bool isDestructible) : base(model)
        {
            _isDestructible = isDestructible;
        }

        public override void Accept(ITileVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}