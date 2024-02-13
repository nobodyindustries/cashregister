using System.Globalization;
using System.Reflection;
using System.Text;
using CashRegister.Management.RuleEngine;

namespace CashRegister.Management;

public class Basket
{
    public List<BasketItem> Content { get; } = [];
    public List<IBasketRule> DiscountRules = [];

    private void ReloadRules()
    {
        // Dynamically load and instantiate all the rules
        var ruleInstances = 
            from t in Assembly.GetExecutingAssembly().GetTypes()
            where t.GetInterfaces().Contains(typeof(IBasketRule))
                  && t.GetConstructor(Type.EmptyTypes) != null
            select Activator.CreateInstance(t) as IBasketRule;
        DiscountRules = ruleInstances.ToList();
    }
    
    public Basket()
    {
        ReloadRules();
    }

    public void Clear()
    {
        Content.Clear();
        ReloadRules();
    }

    public void AddProduct(Product p)
    {
        if (Content.Any(bt => bt.Item.Equals(p)))
        {
            var item = Content.Find(bt => bt.Item.Equals(p));
            if (item != null)
            {
                item.Quantity++;
            }
        }
        else
        {
            Content.Add(new BasketItem(p));
        }
    }

    private decimal CalculateDiscounts()
    {
        var totalDiscountInCents = DiscountRules
            .Where(rule => rule.Applies(this))
            .Sum(rule => rule.GetAmountInCents(this));
        var discountInUnits = Convert.ToDecimal(totalDiscountInCents / 100.0);
        return decimal.Round(discountInUnits, 2, MidpointRounding.AwayFromZero);
    }

    private bool HasApplicableRules()
    {
        return DiscountRules.Any(rule => rule.Applies(this));
    }

    public decimal CalculateGrandTotal()
    {
        return Content.Sum(basketItem => basketItem.CalculatePrice()) + CalculateDiscounts();
    }

    public override string ToString()
    {
        if (Content.Count == 0) return "\nEmpty basket. Add at least a product to create an invoice\n";
        var sb = new StringBuilder();
        sb.AppendLine();
        sb.AppendLine("= INVOICE =");
        sb.AppendLine();
        sb.AppendLine("- Products -");
        foreach (var basketItem in Content)
        {
            sb.AppendLine(
                $"{basketItem.ToString()}");
        }
        if (HasApplicableRules())
        {
            sb.AppendLine("- Discounts -");
            foreach (var rule in DiscountRules.Where(rule => rule.Applies(this)))
            {
                sb.AppendLine($"{rule.ToString(this)}");
            }
        }
        sb.AppendLine();
        sb.AppendLine($"TOTAL: {CalculateGrandTotal().ToString(CultureInfo.InvariantCulture)}€"); 
        return sb.ToString();
    }
}