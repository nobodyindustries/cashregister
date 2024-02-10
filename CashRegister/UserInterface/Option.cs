using CashRegister.Logger;
using CashRegister.Management;
using Microsoft.Extensions.Logging;

namespace CashRegister.UserInterface;

public delegate void OptionCallback(Application application, Inventory inventory, Invoice invoice);
public class Option
{
    public string Selector { get; }
    public string Title { get; }
    private readonly OptionCallback? _callback;
    
    public Option(string selector, string title, OptionCallback? callback)
    {
        if (selector == "" || title == "")
        {
            new CashRegisterLogger().Log(LogLevel.Critical, $"Invalid values to create option Selector[{selector}] - Title[{title}]");
            throw new ArgumentException($"Invalid values to create option Selector[{selector}] - Title[{title}");
        }
        
        Selector = selector;
        Title = title;
        _callback = callback;
    }

    public override string ToString()
    {
        return $"{Selector} - {Title}";
    }

    private bool Equals(Option other)
    {
        // Options with same title but different selector are considered different
        return Selector == other.Selector;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((Option)obj);
    }

    public override int GetHashCode()
    {
        return Selector.GetHashCode();
    }

    public void Execute(Application application, Inventory inventory, Invoice invoice)
    {
        _callback?.Invoke(application, inventory, invoice);
    }
}