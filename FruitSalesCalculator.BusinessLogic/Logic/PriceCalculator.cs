using FruitSalesCalculator.BusinessLogic.Fruits;
using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator.BusinessLogic.Logic
{
    public static class PriceCalculator
    {
        public static PriceCalculationResult CalculateOrderPrice(Order order)
        {
            if (order == null || order.Items == null || !order.Items.Any())
            {
                throw new ArgumentException("Order cannot be null or empty.");
            }

            var result = new PriceCalculationResult();
            foreach (var item in order.Items)
            {
                var fruit = FruitFactory.GetFruit(item.Name);
                var price = fruit.GetPrice(item.Quantity, item.WeightInKg, order.CustomerDiscountType);
                result.Items.Add(new PriceCalculationResultItem
                {
                    Name = fruit.Name,
                    Quantity = item.Quantity,
                    WeightInKg = item.WeightInKg,
                    Price = price.Total,
                    Discount = price.Discount
                });
            }
            return result;
        }
    }
}
