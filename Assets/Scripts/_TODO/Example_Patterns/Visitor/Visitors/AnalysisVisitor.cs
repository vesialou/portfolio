namespace Game.Features.Visitor
{
    public class AnalysisVisitor : ITileVisitor
    {
        public void Visit(GemTile tile)
        {
            // todo: analyze gem matching potential and point value
        }

        public void Visit(BlockerTile tile)
        {
            // todo: analyze blocker durability and breaking strategy
        }

        public void Visit(ChestTile tile)
        {
            // todo: analyze chest unlock requirements and reward value
        }
    }
}