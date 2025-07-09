namespace Game.Features.Visitor
{
    public class HintVisitor : ITileVisitor
    {
        public void Visit(GemTile tile)
        {
            if (tile.Model.IsActive)
            {
                // todo: show possible match directions and point preview
            }
        }

        public void Visit(BlockerTile tile)
        {
            if (tile.Model.IsActive && tile.IsDestructible)
            {
                // todo: highlight adjacent gems that can damage blocker
            }
        }

        public void Visit(ChestTile tile)
        {
            if (tile.Model.IsActive && !tile.IsOpened)
            {
                // todo: pulse chest and show unlock requirement
            }
        }
    }
}