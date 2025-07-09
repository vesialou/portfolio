using System.Collections.Generic;

namespace Game.Features.Visitor
{
    public class TileInteractionController
    {
        private readonly TileVisitorService _visitorService;
        private readonly List<BaseTile> _gameTiles;

        public TileInteractionController(TileVisitorService tileVisitorService)
        {
            _visitorService = tileVisitorService;
            _gameTiles = new List<BaseTile>();
        }

        public void InitializeTiles()
        {
            _gameTiles.Add(new GemTile(new TileModel(TileType.Gem, new TilePosition(0, 0)), "Red"));
            _gameTiles.Add(new GemTile(new TileModel(TileType.Gem, new TilePosition(1, 0)), "Blue"));
            _gameTiles.Add(new BlockerTile(new TileModel(TileType.Blocker, new TilePosition(2, 0)), true));
            _gameTiles.Add(new ChestTile(new TileModel(TileType.Chest, new TilePosition(3, 0)), "Gold"));
        }

        public void HighlightAllTiles()
        {
            var highlightVisitor = new HighlightVisitor();
            _visitorService.ApplyVisitorToTiles(_gameTiles, highlightVisitor);
            // todo: visitor effects are applied directly to tiles
        }

        public void ShowHintsForTiles()
        {
            var hintVisitor = new HintVisitor();
            _visitorService.ApplyVisitorToTiles(_gameTiles, hintVisitor);
            // todo: hint effects are applied directly to tiles
        }

        public void HighlightSpecificTileType(TileType tileType)
        {
            var highlightVisitor = new HighlightVisitor();
            _visitorService.ProcessTilesByType(_gameTiles, tileType, highlightVisitor);
            // todo: filtered highlight effects are applied
        }

        public IReadOnlyList<BaseTile> GetTiles() => _gameTiles;
    }
}