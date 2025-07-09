namespace Game.Features.Visitor
{
    public abstract class BaseTile
    {
        protected TileModel _model;
        public TileModel Model => _model;

        protected BaseTile(TileModel model)
        {
            _model = model;
        }

        public abstract void Accept(ITileVisitor visitor);
    }
}