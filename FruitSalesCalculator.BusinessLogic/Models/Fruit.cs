using FruitSalesCalculator.BusinessLogic.Exceptions;
using FruitSalesCalculator.BusinessLogic.PricingMethods;

namespace FruitSalesCalculator.BusinessLogic.Models
{
    public abstract class Fruit(string name, decimal basePrice, IPricingMethod pricingMethod)
    {
        public string Name { get; init; } = name;
        public decimal BasePrice { get; init; } = basePrice;
        public IPricingMethod PricingMethod { get; init; } = pricingMethod;
        

        public Price GetPrice(int quantity, decimal weightInKg, CustomerDiscountType customerDiscountType)
        {
            try
            {
                var price = PricingMethod.CalculatePrice(BasePrice, quantity, weightInKg);
                var discount = PricingMethod.CalculateDiscount(price, quantity, weightInKg, customerDiscountType);
                return new()
                {
                    Total = price - discount,
                    Discount = discount
                };
            }
            catch (PriceException ex)
            {
                throw new PriceException($"Error calculating the price for {Name}: {ex.Message}");
            }
        }
    }
}
