using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator.BusinessLogic.PricingMethods
{
    public class PricePerKgWithWeightDiscount : PricePerKg
    {
        public override decimal CalculateDiscount(decimal totalPrice, int quantity, decimal weightInKg, CustomerDiscountType customerDiscountType)
        {
            var weightThresholdForDiscount = 2;
            var discountPercentage = .1m;
            return weightInKg > weightThresholdForDiscount ? totalPrice * discountPercentage : 0;
        }
    }
}
