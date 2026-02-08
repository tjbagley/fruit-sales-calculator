namespace FruitSalesCalculator.BusinessLogic.Models
{
    public class PriceCalculationResult
    {
        public List<PriceCalculationResultItem> Items { get; set; } = [];

        public decimal TotalCost => Items.Sum(i => i.Price);
    }
}
