using FruitSalesCalculator.BusinessLogic.Logic;
using FruitSalesCalculator.BusinessLogic.Models;
using FruitSalesCalculator.BusinessLogic.Exceptions;

namespace FruitSalesCalculator.Tests.Logic
{
    public class PriceCalculatorTests
    {
        [Fact]
        public void CalculateOrderPrice_ValidOrder_ReturnsExpectedItemPricesAndTotal()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Name = "apple", Quantity = 0, WeightInKg = 1m },
                    new() { Name = "banana", Quantity = 3, WeightInKg = 0 },
                    new() { Name = "Cherry", Quantity = 300, WeightInKg = 3.76m },
                ]
            };

            var result = PriceCalculator.CalculateOrderPrice(order);

            Assert.NotNull(result);
            Assert.Equal(3, result.Items.Count);

            var appleResult = result.Items[0];
            Assert.Equal("Apple", appleResult.Name);
            Assert.Equal(0, appleResult.Quantity);
            Assert.Equal(1m, appleResult.WeightInKg);
            Assert.Equal(2.0m, appleResult.Price);
            Assert.Equal(0m, appleResult.Discount);

            var bananaResult = result.Items[1];
            Assert.Equal("Banana", bananaResult.Name);
            Assert.Equal(3, bananaResult.Quantity);
            Assert.Equal(0m, bananaResult.WeightInKg);
            Assert.Equal(0.9m, bananaResult.Price);
            Assert.Equal(0m, bananaResult.Discount);

            var cherryResult = result.Items[2];
            Assert.Equal("Cherry", cherryResult.Name);
            Assert.Equal(300, cherryResult.Quantity);
            Assert.Equal(3.76m, cherryResult.WeightInKg);
            Assert.Equal(16.92m, cherryResult.Price);
            Assert.Equal(1.88m, cherryResult.Discount);

            Assert.Equal(19.82m, result.TotalCost);
        }

        [Fact]
        public void CalculateOrderPrice_NullOrder_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => PriceCalculator.CalculateOrderPrice(null));
        }

        [Fact]
        public void CalculateOrderPrice_EmptyOrderItems_ThrowsArgumentException()
        {
            var order = new Order { Items = [] };
            Assert.Throws<ArgumentException>(() => PriceCalculator.CalculateOrderPrice(order));
        }

        [Fact]
        public void CalculateOrderPrice_UnsupportedFruit_ThrowsArgumentException()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Name = "mango", Quantity = 1, WeightInKg = 1m }
                ]
            };

            var ex = Assert.Throws<ArgumentException>(() => PriceCalculator.CalculateOrderPrice(order));
            Assert.Contains("mango", ex.Message, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void CalculateOrderPrice_PricingThrowsPriceException_IsPropagatedWithFruitContext()
        {
            var order = new Order
            {
                Items =
                [
                    new() { Name = "apple", Quantity = 1, WeightInKg = 0m }
                ]
            };

            var ex = Assert.Throws<PriceException>(() => PriceCalculator.CalculateOrderPrice(order));
            Assert.Contains("Error calculating the price for", ex.Message);
            Assert.Contains("Apple", ex.Message, StringComparison.OrdinalIgnoreCase);
        }
    }
}
