using System.Text;
using CashRegister.Logger;
using CashRegister.Management;
using Microsoft.Extensions.Logging;

namespace CashRegister.UserInterface;

public class Menu(string title)
{
    private readonly Dictionary<string, Option> _options = new();

    private void CheckOptionAlreadyExisting(Option o)
    {
        var r = _options.ContainsValue(o);
        if (r) return;
        new CashRegisterLogger().Log(LogLevel.Critical, $"Duplicated option: Selector[{o.Selector}] - Title[{o.Title}]");
        throw new ArgumentException($"Invalid values to create option Selector[{o.Selector}] - Title[{o.Title}");
    }

    public void AddOption(Option o)
    {
        CheckOptionAlreadyExisting(o);
        _options.Add(o.Selector, o);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine("");
        sb.AppendLine($"= {title} =");
        sb.AppendLine("");
        foreach(var o in _options.Values)
        {
            sb.AppendLine(o.ToString());
        }
        sb.AppendLine("");
        return sb.ToString();
    }

    private bool ValidOption(string? input)
    {
        return input != null && _options.Values.Any((option) => option.Selector == input);
    }

    public void Prompt(Navigation navigation, Inventory inventory, Invoice invoice)
    {
        string? currentInput = null;
        while (currentInput == null || !ValidOption(currentInput))
        {
            Console.Write(">");
            currentInput = Console.ReadLine();
        }

        _options[currentInput].Execute(navigation, inventory, invoice);
    }
    
}