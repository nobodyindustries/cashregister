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

    private bool Equals(Product other)
    {
        return ProductCode == other.ProductCode && ProductName == other.ProductName && PriceInCents == other.PriceInCents;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProductCode, ProductName, PriceInCents);
    }
}