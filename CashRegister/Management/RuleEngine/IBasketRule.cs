using System.Globalization;

namespace CashRegister.Management.RuleEngine;

public interface IBasketRule
{
    public string GetDescription();
    public bool Applies(Basket basket);
    
    // It can be a discount (negative amount) or penalty (positive amount)
    public int GetAmountInCents(Basket basket);

    public string ToString(Basket basket)
    {
        var discountInUnits = Convert.ToDecimal(GetAmountInCents(basket) / 100.0);
        decimal.Round(discountInUnits, 2, MidpointRounding.AwayFromZero);
        return $"{GetDescription()} -> {discountInUnits.ToString(CultureInfo.InvariantCulture)}€";
    }
}