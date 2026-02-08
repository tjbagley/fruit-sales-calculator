using FruitSalesCalculator.BusinessLogic.Fruits;
using FruitSalesCalculator.BusinessLogic.PricingMethods;

namespace FruitSalesCalculator.Tests.Fruits
{
    public class FruitFactoryTests
    {
        [Fact]
        public void GetFruit_Apple_ReturnsAppleWithCorrectProperties()
        {
            var fruit = FruitFactory.GetFruit("apple");
            var apple = Assert.IsType<Apple>(fruit);

            Assert.Equal("Apple", apple.Name);
            Assert.Equal(2m, apple.BasePrice);
            Assert.IsType<PricePerKg>(apple.PricingMethod);
        }

        [Fact]
        public void GetFruit_Banana_ReturnsBananaWithCorrectProperties()
        {
            var fruit = FruitFactory.GetFruit("banana");
            var banana = Assert.IsType<Banana>(fruit);

            Assert.Equal("Banana", banana.Name);
            Assert.Equal(0.3m, banana.BasePrice);
            Assert.IsType<PricePerItem>(banana.PricingMethod);
        }

        [Fact]
        public void GetFruit_Orange_ReturnsOrangeWithCorrectProperties()
        {
            var fruit = FruitFactory.GetFruit("orange");
            var orange = Assert.IsType<Orange>(fruit);

            Assert.Equal("Orange", orange.Name);
            Assert.Equal(8.9m, orange.BasePrice);
            Assert.IsType<PricePerKg>(orange.PricingMethod);
        }

        [Fact]
        public void GetFruit_Cherry_ReturnsCherryWithCorrectProperties()
        {
            var fruit = FruitFactory.GetFruit("cherry");
            var cherry = Assert.IsType<Cherry>(fruit);

            Assert.Equal("Cherry", cherry.Name);
            Assert.Equal(5m, cherry.BasePrice);
            Assert.IsType<PricePerKgWithWeightDiscount>(cherry.PricingMethod);
        }

        [Fact]
        public void GetFruit_IsCaseInsensitive()
        {
            var fruit = FruitFactory.GetFruit("APPLE");
            Assert.IsType<Apple>(fruit);
        }

        [Fact]
        public void GetFruit_UnknownFruit_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => FruitFactory.GetFruit("mango"));
            Assert.Contains("mango", ex.Message);
        }
    }
}
