namespace CashRegister.Management.RuleEngine.Rules;

public class BasketRulePriceDropCoffee: IBasketRule
{
    public string GetDescription()
    {
        return "Coffee volume discount (VP of Eng. offer)";
    }

    private static int AmountCount(Basket basket)
    {
        return Convert.ToInt32(basket.Content
            .Where((basketItem) => basketItem.Item.ProductCode.StartsWith("CF"))
            .Sum((basketItem) => basketItem.Quantity));
    }
    
    public bool Applies(Basket basket)
    {
        /*
         * For this rule we assume that everything that has a product code starting with CF is coffee and hence applies
         * towards the count of this rule.
         */
         return AmountCount(basket) >= 3;
    }

    public int GetAmountInCents(Basket basket)
    {
        var totalProductAmount = AmountCount(basket);
        if (totalProductAmount < 3) return 0;
        var basketItemList = basket.Content.FindAll(basketItem => basketItem.Item.ProductCode.StartsWith("CF")).ToList();
        // The discount is one third of the price for every product
        var totalDiscount = basketItemList.Sum(basketItem => basketItem.Item.PriceInCents / 3.0 * basketItem.Quantity);
        return -Convert.ToInt32(double.Round(totalDiscount));
    }
}