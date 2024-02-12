namespace CashRegister.Management.RuleEngine.Rules;

// ReSharper disable once UnusedType.Global
public class BasketRulePriceDropStrawberries: IBasketRule
{
    public string GetDescription()
    {
        return "Strawberry volume discount (COO Offer)";
    }

    private static int AmountCount(Basket basket)
    {
        return Convert.ToInt32(basket.Content
            .Where((basketItem) => basketItem.Item.ProductCode.StartsWith("SR1"))
            .Sum((basketItem) => basketItem.Quantity));
    }
    
    public bool Applies(Basket basket)
    {
        return AmountCount(basket) >= 3;
    }

    public int GetAmountInCents(Basket basket)
    {
        var totalProductAmount = AmountCount(basket);
        if (totalProductAmount < 3) return 0;
        var product  = basket.Content.Find((basketItem => basketItem.Item.ProductCode == "SR1"))?.Item;
        if (product == null) return 0;
        // The total discount should be the difference between the price and the new price times the amount
        return -((product.PriceInCents - 450) * totalProductAmount);
    }
}