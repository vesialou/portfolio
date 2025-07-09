using System.Collections.Generic;

namespace Game.Features.Visitor
{
    public class TileVisitorService
    {
        public void ApplyVisitorToTiles(IReadOnlyList<BaseTile> tiles, ITileVisitor visitor)
        {
            for (var i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                tile.Accept(visitor);
            }
        }

        public void ProcessTilesByType(IReadOnlyList<BaseTile> tiles, TileType targetType, ITileVisitor visitor)
        {
            for (var i = 0; i < tiles.Count; i++)
            {
                var tile = tiles[i];
                if (tile.Model.Type == targetType)
                {
                    tile.Accept(visitor);
                }
            }
        }
    }
}