namespace Game.Features.Visitor
{
    public interface ITileVisitor
    {
        void Visit(GemTile tile);
        void Visit(BlockerTile tile);
        void Visit(ChestTile tile);
    }
}