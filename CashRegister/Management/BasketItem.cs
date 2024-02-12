using System.Globalization;

namespace CashRegister.Management;

public class BasketItem(Product item)
{
    public Product Item { get; } = item;
    public uint Quantity { get; set; } = 1;

    public decimal CalculatePrice()
    {
        var priceInUnits = Convert.ToDecimal(Item.PriceInCents * Quantity / 100.0);
        return decimal.Round(priceInUnits, 2, MidpointRounding.AwayFromZero);
    }

    public override string ToString()
    {
        var individualPriceInUnits = Convert.ToDecimal(Item.PriceInCents / 100.0);
        decimal.Round(individualPriceInUnits, 2, MidpointRounding.AwayFromZero);
        return $"{Item.ProductName} ({individualPriceInUnits.ToString(CultureInfo.InvariantCulture)}€) x {Quantity} = {CalculatePrice().ToString(CultureInfo.InvariantCulture)}€";
    }
}