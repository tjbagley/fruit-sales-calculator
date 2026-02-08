namespace FruitSalesCalculator.BusinessLogic.Models
{
    public record OrderItem
    {
        public required string Name { get; init; }
        public required int Quantity { get; init; }
        public required decimal WeightInKg { get; init; }
    }
}
