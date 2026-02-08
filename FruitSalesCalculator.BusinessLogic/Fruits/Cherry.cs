using FruitSalesCalculator.BusinessLogic.Models;
using FruitSalesCalculator.BusinessLogic.PricingMethods;

namespace FruitSalesCalculator.BusinessLogic.Fruits
{
    public class Cherry() : Fruit("Cherry", 5m, new PricePerKgWithWeightDiscount())
    {
    }
}
