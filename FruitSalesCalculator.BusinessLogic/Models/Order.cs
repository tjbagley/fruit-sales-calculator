namespace FruitSalesCalculator.BusinessLogic.Models
{
    public record Order
    {
        public IList<OrderItem> Items { get; set; } = [];
        public CustomerDiscountType CustomerDiscountType { get; set; } = CustomerDiscountType.None;
    }
}
