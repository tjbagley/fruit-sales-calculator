using FruitSalesCalculator.BusinessLogic.PricingMethods;
using FruitSalesCalculator.BusinessLogic.Exceptions;
using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator.Tests.Logic
{
    public class PriceMethodsTests
    {
        [Fact]
        public void PricePerItem_CalculatePrice_ReturnsBasePriceTimesQuantity()
        {
            var sut = new PricePerItem();
            decimal basePrice = 0.3m;
            int quantity = 4;

            var price = sut.CalculatePrice(basePrice, quantity, 0m);

            Assert.Equal(basePrice * quantity, price);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void PricePerItem_NonPositiveQuantity_ThrowsPriceException(int quantity)
        {
            var sut = new PricePerItem();

            var ex = Assert.Throws<PriceException>(() => sut.CalculatePrice(1m, quantity, 0m));
            Assert.Contains("Quantity must be entered.", ex.Message);
        }

        [Fact]
        public void PricePerItem_CalculateDiscount_ReturnsZero()
        {
            var sut = new PricePerItem();

            var discount = sut.CalculateDiscount(10m, 2, 0m, CustomerDiscountType.None);

            Assert.Equal(0m, discount);
        }

        [Fact]
        public void PricePerKg_CalculatePrice_ReturnsBasePriceTimesWeight()
        {
            var sut = new PricePerKg();
            decimal basePrice = 5m;
            decimal weight = 1.5m;

            var price = sut.CalculatePrice(basePrice, 1, weight);

            Assert.Equal(basePrice * weight, price);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-0.1)]
        public void PricePerKg_NonPositiveWeight_ThrowsPriceException(decimal weight)
        {
            var sut = new PricePerKg();

            var ex = Assert.Throws<PriceException>(() => sut.CalculatePrice(2m, 1, weight));
            Assert.Contains("Weight must be entered.", ex.Message);
        }

        [Fact]
        public void PricePerKg_CalculateDiscount_ReturnsZero()
        {
            var sut = new PricePerKg();

            var discount = sut.CalculateDiscount(20m, 1, 2m, CustomerDiscountType.None);

            Assert.Equal(0m, discount);
        }

        [Fact]
        public void PricePerKgWithWeightDiscount_DiscountAppliedWhenWeightGreaterThanThreshold()
        {
            var sut = new PricePerKgWithWeightDiscount();
            decimal basePrice = 5m;
            decimal weight = 3m;
            var totalPrice = sut.CalculatePrice(basePrice, 1, weight);

            var discount = sut.CalculateDiscount(totalPrice, 1, weight, CustomerDiscountType.None);

            Assert.Equal(totalPrice * 0.1m, discount);
        }

        [Theory]
        [InlineData(2.0)]
        [InlineData(1.99)]
        public void PricePerKgWithWeightDiscount_NoDiscountWhenWeightAtOrBelowThreshold(decimal weight)
        {
            var sut = new PricePerKgWithWeightDiscount();
            decimal basePrice = 5m;
            var totalPrice = sut.CalculatePrice(basePrice, 1, weight);

            var discount = sut.CalculateDiscount(totalPrice, 1, weight, CustomerDiscountType.None);

            Assert.Equal(0m, discount);
        }
    }
}