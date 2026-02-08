namespace FruitSalesCalculator.Tests.Console
{
    public class InputParserTests
    {
        [Fact]
        public void ParseOrder_ValidSingleItem_WithWeightInKg_ParsedCorrectly()
        {
            var order = InputParser.ParseOrder("apple 2 1kg");

            Assert.NotNull(order);
            Assert.Single(order.Items);
            var item = order.Items[0];
            Assert.Equal("apple", item.Name);
            Assert.Equal(2, item.Quantity);
            Assert.Equal(1m, item.WeightInKg);
        }

        [Fact]
        public void ParseOrder_ValidSingleItem_WithWeightInGramsConvertedToKg()
        {
            var order = InputParser.ParseOrder("banana 3 500g");

            Assert.NotNull(order);
            Assert.Single(order.Items);
            var item = order.Items[0];
            Assert.Equal("banana", item.Name);
            Assert.Equal(3, item.Quantity);
            Assert.Equal(0.5m, item.WeightInKg);
        }

        public void ParseOrder_ValidSingleItem_WithJustWeight_ParsedCorrectly()
        {
            var order = InputParser.ParseOrder("apple 1kg");

            Assert.NotNull(order);
            Assert.Single(order.Items);
            var item = order.Items[0];
            Assert.Equal("apple", item.Name);
            Assert.Equal(0, item.Quantity);
            Assert.Equal(1m, item.WeightInKg);
        }

        public void ParseOrder_ValidSingleItem_WithJustQuantity_ParsedCorrectly()
        {
            var order = InputParser.ParseOrder("banana 7");

            Assert.NotNull(order);
            Assert.Single(order.Items);
            var item = order.Items[0];
            Assert.Equal("banana", item.Name);
            Assert.Equal(7, item.Quantity);
            Assert.Equal(0, item.WeightInKg);
        }

        [Fact]
        public void ParseOrder_MultipleItems_ReturnsAllItems()
        {
            var order = InputParser.ParseOrder("apple 1 1kg, orange 500g");

            Assert.NotNull(order);
            Assert.Equal(2, order.Items.Count);

            var a = order.Items[0];
            Assert.Equal("apple", a.Name);
            Assert.Equal(1, a.Quantity);
            Assert.Equal(1m, a.WeightInKg);

            var o = order.Items[1];
            Assert.Equal("orange", o.Name);
            Assert.Equal(0, o.Quantity);
            Assert.Equal(0.5m, o.WeightInKg);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void ParseOrder_NullOrWhitespace_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => InputParser.ParseOrder(input));
        }

        [Fact]
        public void ParseOrder_MissingCommaBetweenItems_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple 1 2kg orange 3 0.5kg"));
        }

        [Fact]
        public void ParseOrder_TooFewParts_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple"));
        }

        [Fact]
        public void ParseOrder_InvalidFruitName_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple1 1 1kg"));
        }

        [Fact]
        public void ParseOrder_InvalidQuantity_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple one 1kg"));
        }

        [Fact]
        public void ParseOrder_NonPositiveQuantity_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("banana 0"));
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("banana -1"));
        }

        [Fact]
        public void ParseOrder_InvalidWeightUnit_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple 100lb"));
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple 3 100lb"));
        }

        [Fact]
        public void ParseOrder_InvalidWeightValue_ThrowsFormatException()
        {
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple abcg"));
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple 0kg"));
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple -10g"));
            Assert.Throws<FormatException>(() => InputParser.ParseOrder("apple 2 -10g"));
        }
    }
}