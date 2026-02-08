using FruitSalesCalculator.BusinessLogic.Exceptions;
using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator.BusinessLogic.PricingMethods
{
    public class PricePerKg : IPricingMethod
    {
        public virtual decimal CalculatePrice(decimal basePrice, int quantity, decimal weightInKg)
        {
            if (weightInKg <= 0)
            {
                throw new PriceException("Weight must be entered.");
            }
            return basePrice * weightInKg;
        }

        public virtual decimal CalculateDiscount(decimal totalPrice, int quantity, decimal weightInKg, CustomerDiscountType customerDiscount)
        {
            return 0;
        }
    }
}
