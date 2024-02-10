using CashRegister.Logger;
using Microsoft.Extensions.Logging;

namespace CashRegister.Navigation;

public class Option
{
    public string Selector { get; }
    public string Title { get; }

    public Option(string selector, string title)
    {
        if (selector == "" || title == "")
        {
            new CashRegisterLogger().Log(LogLevel.Critical, $"Invalid values to create option Selector[{selector}] - Title[{title}]");
            throw new ArgumentException($"Invalid values to create option Selector[{selector}] - Title[{title}");
        }

        Selector = selector;
        Title = title;
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
}