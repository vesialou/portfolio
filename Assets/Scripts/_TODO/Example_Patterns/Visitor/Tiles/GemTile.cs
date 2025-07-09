namespace Game.Features.Visitor
{
    public class GemTile : BaseTile
    {
        private readonly string _gemColor;
        public string GemColor => _gemColor;

        public GemTile(TileModel model, string gemColor) : base(model)
        {
            _gemColor = gemColor;
        }

        public override void Accept(ITileVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}