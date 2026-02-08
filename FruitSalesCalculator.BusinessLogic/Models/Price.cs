namespace FruitSalesCalculator.BusinessLogic.Models
{
    public record Price
    {
        public decimal Total { get; init; }
        public decimal Discount { get; init; }
    }
}
