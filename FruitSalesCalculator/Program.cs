using FruitSalesCalculator;
using FruitSalesCalculator.BusinessLogic.Logic;
using Spectre.Console;

AnsiConsole.MarkupLine("[blue bold]Fruit Sales Calculator[/]");

var errorInLastInput = false;
var firstInput = true;

while (true)
{
    if (!errorInLastInput)
    {
        AnsiConsole.MarkupLine("\n\nPlease type the order details below (or q to quit) and press enter.");
        if (firstInput)
        {
            AnsiConsole.MarkupLine("[#7F7F7F]To enter a fruit by weight use: apple 100g[/]");
            AnsiConsole.MarkupLine("[#7F7F7F]To enter a fruit by quantity use: banana 5[/]");
            AnsiConsole.MarkupLine("[#7F7F7F]Commas should be used to list multiple fruits e.g. apple 200g, orange 2kg, banana 5[/]");
            AnsiConsole.MarkupLine("[#7F7F7F]Use the singular name of the fruit, not the plural e.g. cherry instead of cherries[/]");
        }
    }

    errorInLastInput = false;
    string? input = AnsiConsole.Ask<string>("Order details:");
    firstInput = false;

    if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase) || string.Equals(input, "quit", StringComparison.OrdinalIgnoreCase))
    {
        AnsiConsole.MarkupLine("[red]Quitting Fruit Sales Calculator[/]");
        break;
    }

    try
    {
        var order = InputParser.ParseOrder(input);
        var priceResult = PriceCalculator.CalculateOrderPrice(order);

        var table = new Table()
        .RoundedBorder()
        .BorderColor(Color.Grey)
        .Title("[blue bold]Order Pricing[/]");

        table.AddColumn("Fruit");
        table.AddColumn("Quantity", col => col.RightAligned());
        table.AddColumn("Weight (kg)", col => col.RightAligned());
        table.AddColumn("Discount", col => col.RightAligned());
        table.AddColumn("Price", col => col.RightAligned());

        foreach (var item in priceResult.Items)
        {
            table.AddRow(item.Name, item.Quantity > 0 ? item.Quantity.ToString() : "", item.WeightInKg > 0 ? item.WeightInKg.ToString("F2") : "", item.Discount != 0 ? item.Discount.ToString("C2") : "", item.Price.ToString("C2"));
        }

        // Add footer with totals
        table.Columns[0].Footer = new Text("Total Cost", new Style(decoration: Decoration.Bold));
        table.Columns[1].Footer = new Text("");
        table.Columns[2].Footer = new Text("");
        table.Columns[3].Footer = new Text("");
        table.Columns[4].Footer = new Text(priceResult.TotalCost.ToString("C2"), new Style(Color.Green, decoration: Decoration.Bold));

        AnsiConsole.MarkupLine("");
        AnsiConsole.Write(table);
    }
    catch (Exception ex)
    {
        errorInLastInput = true;
        AnsiConsole.MarkupLineInterpolated($"[red]{ex.Message}[/]\n");
    }
}

