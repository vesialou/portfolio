namespace Game.Features.Visitor
{
    public class TileModel
    {
        public TileType Type { get; }
        public TilePosition Position { get; }
        public bool IsActive { get; set; }

        public TileModel(TileType type, TilePosition position)
        {
            Type = type;
            Position = position;
            IsActive = true;
        }
    }
}