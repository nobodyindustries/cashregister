namespace CashRegister.Management.RuleEngine;

public class BasketRuleComparer: IEqualityComparer<IBasketRule>
{
    public bool Equals(IBasketRule? x, IBasketRule? y)
    {
        return string.Equals(x?.GetDescription(), y?.GetDescription());
    }

    public int GetHashCode(IBasketRule obj)
    {
        return HashCode.Combine(obj.GetDescription());
    }
}