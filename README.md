# Fruit Sales Calculator

## Technical Details

### Application Type

Console App

### .NET Version

10

## Running the App

### Visual Studio

The solution can be loaded and run from inside Visual Studio by going to the debug menu and choosing "Start Without Debugging".

### Command Line

Navigate to the FruitSalesCalculator project directory
```
dotnet run
```

## How To Use

At the prompt, enter the order details and press enter. The total cost of the order will then be displayed.     
The input should include the name of the fruit followed by either the quantity or the weight.   
Multiple fruits should be separated by commas.   
e.g.   
```
Apple 500g, Cherry 3kg, Banana 4
```

## Design Patterns Used

* Factory - so the instance of each fruit can be created based on the name passed in the order
* Strategy - to allow different pricing methods to be configurable for each fruit

## Design Decisions

* Used a console application as it seemed the simplest option to meet the requirements
* Separated the business logic into its own project so it could be re-used if it was decided to switch the solution to an API or an ASP.NET UI
* Used a switch for the Fruit Factory instead of reflection as it allowed better case insensitive matches on the fruit name.

## Extending

### New Fruits

New fruit can be added by creating a class that extends the Fruit base class. The fruit name, base price and pricing method will need to be setup.

```C#
using FruitSalesCalculator.BusinessLogic.Models;
using FruitSalesCalculator.BusinessLogic.PricingMethods;

namespace FruitSalesCalculator.BusinessLogic.Fruits
{
    public class Orange(): Fruit("Orange", 8.9m, new PricePerKg())
    {
    }
}
```

### New Pricing Methods And Discounts

New pricing methods can be added by creating a class that implements the IPricingMethod interface.  
If it's only a new discount that's required, you can extend one of the existing pricing methods of PricePerKg and PricePerItem and override the CalculateDiscount method.

Once a new pricing method is created you can assign it to a fruit in the fruit's constructor.

```C#
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
```

## Assumptions

* The price and discount information for each fruit will be stored in code and won't come from a database.
* Printing the total cost refers to on screen, not printing to paper.