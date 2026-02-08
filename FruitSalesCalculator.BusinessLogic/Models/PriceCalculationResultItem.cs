namespace FruitSalesCalculator.BusinessLogic.Models
{
    public record PriceCalculationResultItem : OrderItem
    {
        public required decimal Price { get; init; }
        public required decimal Discount { get; init; }
    }
}
