namespace CashRegister.Management.RuleEngine.Rules;

// ReSharper disable once UnusedType.Global
public class BasketRuleBuyTwoGetOneFreeGreenTea: IBasketRule
{
    public string GetDescription()
    {
        return "2x1 Green Tea (CEO Offer)";
    }

    private static int AmountCount(Basket basket)
    {
        return Convert.ToInt32(basket.Content
            .Where((basketItem) => basketItem.Item.ProductCode == "GR1")
            .Sum((basketItem) => basketItem.Quantity));
    }
    
    public bool Applies(Basket basket)
    {
        return AmountCount(basket) >= 2;
    }

    public int GetAmountInCents(Basket basket)
    {
        var totalProductAmount = AmountCount(basket);
        if (totalProductAmount < 2) return 0;
        var product  = basket.Content.Find((basketItem => basketItem.Item.ProductCode == "GR1"))?.Item;
        if (product == null) return 0;
        var freeUnitAmount = totalProductAmount / 2; // Integer division
        return -(freeUnitAmount * product.PriceInCents);
    }
}