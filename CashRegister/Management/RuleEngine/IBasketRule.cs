using System.Globalization;
using CashRegister.Management.RuleEngine.Rules;

namespace CashRegister.Management.RuleEngine;


public interface IBasketRule
{
    public static readonly Dictionary<Type, int> BasketRuleIdentifiers = new()
        {
            { typeof(BasketRuleTestImplementation), 0 },
            { typeof(BasketRulePriceDropStrawberries), 1 },
            { typeof(BasketRulePriceDropCoffee), 2 },
            { typeof(BasketRuleBuyTwoGetOneFreeGreenTea), 3 }
        };
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