using System.Text.RegularExpressions;
using CashRegister.Management;
using CashRegister.UserInterface;

namespace TestCashRegister.TestUserInterface;

[TestFixture]
public partial class TestApplication
{
    
    private readonly Inventory _inventory = new("./TestData/valid_products.csv");
    private readonly Basket _basket = new();
    
    [GeneratedRegex("(.*)MAIN MENU(.*)(ADD PRODUCT TO INVOICE(.*)){2}(MAIN MENU(.*)){2}", RegexOptions.Singleline)]
    private static partial Regex NavigationSuccessfulRegex();
    
    [Test]
    public void TestApplicationRun()
    {
        var app = new Application(_inventory, _basket);
        
        var sw = new StringWriter();
        Console.SetOut(sw);
        var sr = new StringReader("A\nFD1\nB\nF\nX\n");
        Console.SetIn(sr);
        
        app.Run();

        var consoleOutput = sw.ToString();
        var result = NavigationSuccessfulRegex().IsMatch(consoleOutput);
        Assert.That(result, Is.True);
    }
}