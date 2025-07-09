namespace Game.Features.Visitor
{
    public class HighlightVisitor : ITileVisitor
    {
        public void Visit(GemTile tile)
        {
            if (tile.Model.IsActive)
            {
                // todo: apply gem glow effect with sparkle animation
            }
        }

        public void Visit(BlockerTile tile)
        {
            if (tile.Model.IsActive && tile.IsDestructible)
            {
                // todo: apply red outline effect, show health indicator
            }
        }

        public void Visit(ChestTile tile)
        {
            if (tile.Model.IsActive && !tile.IsOpened)
            {
                // todo: apply golden aura effect, show treasure hint
            }
        }
    }
}