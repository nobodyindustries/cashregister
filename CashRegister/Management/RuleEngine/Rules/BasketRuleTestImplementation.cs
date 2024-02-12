using System.Diagnostics;

namespace CashRegister.Management.RuleEngine.Rules;

// ReSharper disable once UnusedType.Global
public class BasketRuleTestImplementation: IBasketRule
{
    // This rule is only to be executed while testing
    private static readonly bool IsRunningFromNUnit = 
        AppDomain.CurrentDomain.GetAssemblies().Any(
            assembly =>
            {
                Debug.Assert(assembly.FullName != null, "a.FullName != null");
                return assembly.FullName.StartsWith("nunit.framework", StringComparison.InvariantCultureIgnoreCase);
            });
    
    public string GetDescription()
    {
        return IsRunningFromNUnit ? "" : "Test BasketRule for invalid product (No discount)";
    }

    public bool Applies(Basket basket)
    {
        return IsRunningFromNUnit && Convert.ToInt32(basket.Content
            .Where(basketItem => basketItem.Item.ProductCode.Equals("XX"))
            .Sum(basketItem => basketItem.Quantity)) >= 2;
    }

    public int GetAmountInCents(Basket basket)
    {
        // Returns the amount of times the "XX product" has been added as the discount
        return !IsRunningFromNUnit ? 0 : Convert.ToInt32(basket.Content
            .Where(basketItem => basketItem.Item.ProductCode.Equals("XX"))
            .Sum(basketItem => basketItem.Quantity));
    }
}