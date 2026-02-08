using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator.BusinessLogic.PricingMethods
{
    public interface IPricingMethod
    {
        decimal CalculatePrice(decimal basePrice, int quantity, decimal weightInKg);
        decimal CalculateDiscount(decimal totalPrice, int quantity, decimal weightInKg, CustomerDiscountType customerDiscountType);
    }
}
