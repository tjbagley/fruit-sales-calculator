using FruitSalesCalculator.BusinessLogic.Models;
using FruitSalesCalculator.BusinessLogic.PricingMethods;

namespace FruitSalesCalculator.BusinessLogic.Fruits
{
    public class Orange(): Fruit("Orange", 8.9m, new PricePerKg())
    {
    }
}
