namespace Game.Features.Visitor
{
    public struct TilePosition
    {
        public int X { get; }
        public int Y { get; }

        public TilePosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}