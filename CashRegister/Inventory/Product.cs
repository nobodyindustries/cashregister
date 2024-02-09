namespace CashRegister.Inventory;

public class Product(string productCode, string productName, int priceInCents)
{
    private string ProductCode { get; } = productCode;
    private string ProductName { get; } = productName;
    private int PriceInCents { get; } = priceInCents;

    public override string ToString()
    {
        return $"Product -> Code: {ProductCode} | Name: {ProductName} | PriceInCents: {PriceInCents}";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        var p = (Product)obj;
        return ProductCode.Equals(p.ProductCode) && ProductName.Equals(p.ProductName) && PriceInCents == p.PriceInCents;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProductCode, ProductName, PriceInCents);
    }
}