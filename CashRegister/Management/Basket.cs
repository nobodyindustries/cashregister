using System.Globalization;
using System.Text;

namespace CashRegister.Management;

public class Basket
{
    public List<BasketItem> Content { get; } = [];
    
    public void Clear()
    {
        Content.Clear();
    }

    public void AddProduct(Product p)
    {
        if (Content.Any((bt) => bt.Item.Equals(p)))
        {
            var item = Content.Find((bt) => bt.Item.Equals(p));
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

    public void ApplyRules()
    {
        // throw new NotImplementedException();
    }

    private decimal CalculateGrandTotal()
    {
        return Content.Sum(basketItem => basketItem.CalculatePrice());
    }

    public override string ToString()
    {
        if (Content.Count == 0) return "Empty basket. Add at least a product to create an invoice";
        var sb = new StringBuilder();
        sb.AppendLine("= INVOICE =");
        sb.AppendLine();
        sb.AppendLine("- Products -");
        foreach (var basketItem in Content)
        {
            sb.AppendLine(
                $"{basketItem.Item.ToString()} x {basketItem.Quantity} = {basketItem.CalculatePrice().ToString(CultureInfo.InvariantCulture)}€");
        }
        sb.AppendLine("- Discounts -");
        sb.AppendLine();
        sb.AppendLine($"TOTAL: {CalculateGrandTotal()}€");
        return sb.ToString();
    }
}