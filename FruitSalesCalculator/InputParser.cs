using FruitSalesCalculator.BusinessLogic.Models;

namespace FruitSalesCalculator
{
    public static class InputParser
    {
        private static readonly string[] AllowedWeightUnitSymbols = [Constants.KilogramsUnitSymbol, Constants.GramsUnitSymbol];

        public static Order ParseOrder(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input cannot be null or empty.");
            }

            var inputItems = input.Split(',');
            IList<OrderItem> orderItems = [.. inputItems.Select(i => ParseOrderItem(i))];
            return new()
            {
                Items = orderItems
            };
        }

        private static OrderItem ParseOrderItem(string input)
        {
            var itemParts = input.Trim().Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            if (itemParts.Count > 3)
            {
                throw new FormatException($"Invalid input: '{input}'. You might be missing a comma between the order items e.g. apple 2kg, orange 500g");
            }
            else if (itemParts.Count < 2)
            {
                throw new FormatException($"Invalid input: '{input.Trim()}'. It should be in the format: fruit quantity/weight e.g. apple 2kg");
            }

            var fruitName = itemParts[0];
            if (!fruitName.All(char.IsLetter))
            {
                throw new FormatException($"Invalid fruit: '{fruitName}'.");
            }

            int quantity = 0;
            string unitSymbol = string.Empty;
            decimal weightInKg = 0;

            if (itemParts.Count == 2)
            {
                var part = itemParts[1];
                var metricTypeCheck = AllowedWeightUnitSymbols.FirstOrDefault(m => part != null && part.ToLower().EndsWith(m.ToLower()));

                if (metricTypeCheck != null)
                {
                    weightInKg = ParseWeight(part, out unitSymbol);                   
                }
                else
                {
                    quantity = ParseQuantity(part);
                }
            }
            else
            {
                quantity = ParseQuantity(itemParts[1]);
                weightInKg = ParseWeight(itemParts[2], out unitSymbol);
            }

            return new()
            {
                Name = fruitName,
                Quantity = quantity,
                WeightInKg = unitSymbol == Constants.GramsUnitSymbol ? weightInKg / 1000 : weightInKg
            };
        }

        private static int ParseQuantity(string input)
        {
            if (!int.TryParse(input, out int quantity) || quantity <= 0)
            {
                throw new FormatException($"Invalid quantity: '{input}'. Quantity must be a positive integer.");
            }
            return quantity;
        }

        private static decimal ParseWeight(string weight, out string unitSymbol)
        {
            unitSymbol = AllowedWeightUnitSymbols.FirstOrDefault(m => weight != null && weight.ToLower().EndsWith(m.ToLower())) ?? throw new FormatException($"Invalid weight: '{weight}'. Weight must end with one of {string.Join(" or ", AllowedWeightUnitSymbols)}");
            weight = weight.Replace(unitSymbol, string.Empty);
            if (!decimal.TryParse(weight, out decimal weightValue) || weightValue <= 0)
            {
                throw new FormatException($"Invalid weight: '{weight}'.");
            }
            return weightValue;
        }
    }
}
