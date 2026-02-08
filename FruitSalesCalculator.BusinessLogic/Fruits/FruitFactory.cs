using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator.BusinessLogic.Fruits
{
    public class FruitFactory
    {
        public static Fruit GetFruit(string fruitName)
        {
            return fruitName.ToLower() switch
            {
                "apple" => new Apple(),
                "banana" => new Banana(),
                "cherry" => new Cherry(),
                "orange" => new Orange(),
                _ => throw new ArgumentException($"Fruit '{fruitName}' is not supported.")
            };
        }
    }
}
