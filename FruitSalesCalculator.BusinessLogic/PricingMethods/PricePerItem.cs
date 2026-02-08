using FruitSalesCalculator.BusinessLogic.Exceptions;
using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator.BusinessLogic.PricingMethods
{
    public class PricePerItem : IPricingMethod
    {
        public virtual decimal CalculatePrice(decimal basePrice, int quantity, decimal weightInKg)
        {
            if (quantity <= 0)
            {
                throw new PriceException("Quantity must be entered.");
            }
            return basePrice * quantity;
        }

        public virtual decimal CalculateDiscount(decimal totalPrice, int quantity, decimal weightInKg, CustomerDiscountType customerDiscountType)
        {
            return 0;
        }
    }
}
