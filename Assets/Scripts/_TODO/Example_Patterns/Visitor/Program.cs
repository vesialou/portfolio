namespace Game.Features.Visitor
{
    public class Program
    {
        public void RunExample()
        {
            var controller = new TileInteractionController(
                new TileVisitorService());
            controller.InitializeTiles();
            // 1
            controller.HighlightAllTiles();
            // 2
            controller.ShowHintsForTiles();
            // 3
            controller.HighlightSpecificTileType(TileType.Gem);

            // Example of easy extension - add new visitor without modifying existing code
            var customVisitor = new AnalysisVisitor();
            var service = new TileVisitorService();
            service.ApplyVisitorToTiles(controller.GetTiles(), customVisitor);
            // todo: analysis results processed
        }
    }
}