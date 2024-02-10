using System.Text;

namespace CashRegister.Management;

public class Invoice
{
    public void Clear()
    {
        //throw new NotImplementedException();
    }

    public void AddProduct(Product p)
    {
        //throw new NotImplementedException();
    }

    public void ApplyDiscountRules()
    {
        // throw new NotImplementedException();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("= INVOICE =");
        sb.AppendLine();
        return sb.ToString();
    }
}