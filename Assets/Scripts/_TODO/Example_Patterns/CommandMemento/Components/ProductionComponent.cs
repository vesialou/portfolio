namespace Game.Features.CommandMemento
{
    public class ProductionComponent : IBuildingComponent<ProductionSnapshot>
    {
        public string ComponentType => "Production";
        public float Rate { get; set; } = 1.0f;

        public ProductionSnapshot CreateSnapshot()
        {
            return new(Rate);
        }

        public void RestoreFromSnapshot(ProductionSnapshot snapshot)
        {
            Rate = snapshot.Rate;
        }
    }
}