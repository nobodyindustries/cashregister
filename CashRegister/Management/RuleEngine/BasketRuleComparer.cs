using System.Diagnostics;

namespace CashRegister.Management.RuleEngine;

public class BasketRuleComparer: IEqualityComparer<IBasketRule>
{
    public bool Equals(IBasketRule? x, IBasketRule? y)
    {
        // ReSharper disable once ConvertIfStatementToSwitchStatement
        if (x == null && y == null) return true;
        if (x == null && y != null) return false;
        if (x != null && y == null) return false;
        if (x != null && y != null) return IBasketRule.BasketRuleIdentifiers[x.GetType()] == IBasketRule.BasketRuleIdentifiers[y.GetType()];
        throw new UnreachableException();
    }

    public int GetHashCode(IBasketRule obj)
    {
        var typeId = IBasketRule.BasketRuleIdentifiers[obj.GetType()];
        return HashCode.Combine(typeId);
    }
}