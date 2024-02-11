namespace CashRegister.Management;

public sealed class Product(string productCode, string productName, int priceInCents)
{
    public string ProductCode { get; } = productCode;
    public string ProductName { get; } = productName;
    public int PriceInCents { get; } = priceInCents;

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
        return obj.GetType() == GetType() && Equals((Product)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProductCode, ProductName, PriceInCents);
    }
    
    public static int CompareProducts(Product x, Product y)
    {
        return 0;
    }
}