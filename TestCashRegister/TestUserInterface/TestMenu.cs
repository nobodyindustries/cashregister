using CashRegister.Management;
using CashRegister.UserInterface;

namespace TestCashRegister.TestUserInterface;

[TestFixture]
public class TestMenu
{
    
    private readonly Inventory _inventory;
    private readonly Basket _basket;
    private readonly Application? _application;
    
    public TestMenu()
    {
        _inventory = new Inventory("./TestData/valid_products.csv");
        _basket = new Basket();
        _application = new Application(_inventory, _basket);
    }
    
    [Test]
    public void TestMenuToStringNoOptions()
    {
        const string expected = "\n= TITLE =\n\n";
        var m = new Menu("TITLE");
        Assert.That(m.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestMenuToStringOptions()
    {
        const string expected = "\n= TITLE =\n\nSELECTOR - OPTION_TITLE\n\n";
        var m = new Menu("TITLE");
        m.AddOption(new Option("SELECTOR", "OPTION_TITLE", null));
        Assert.That(m.ToString(), Is.EqualTo(expected));
    }
    
    [Test]
    public void TestMenuAddDuplicatedOption()
    {
        var m = new Menu("TITLE");
        m.AddOption(new Option("SELECTOR", "OPTION_TITLE", null));
        Assert.Throws(typeof(ArgumentException), () =>
        {
            m.AddOption(new Option("SELECTOR", "OPTION_TITLE", null));
        });
    }
    
    
    [Test]
    public void TestMenuAddDuplicatedOptionDifferentTitle()
    {
        var m = new Menu("TITLE");
        m.AddOption(new Option("SELECTOR", "OPTION_TITLE", null));
        Assert.Throws(typeof(ArgumentException), () =>
        {
            m.AddOption(new Option("SELECTOR", "OPTION_TITLE_X", null));
        });
    }

    [Test, Timeout(2000)]
    public void TestPromptValidOption()
    {
        var sw = new StringWriter();
        Console.SetOut(sw);
        var sr = new StringReader("SELECTOR");
        Console.SetIn(sr);

        var m = new Menu("TITLE");
        m.AddOption(new Option("SELECTOR", "OPTION_TITLE", null));

        if (_application == null)
        {
            Assert.Fail();
        }
        else
        {
            m.Prompt(_application, _inventory, _basket);
            // The Prompt method should just finish
            Assert.That(sw.ToString(), Is.EqualTo("> "));
        }
    }
    
    [Test, Timeout(2000)]
    public void TestPromptInvalidOption()
    {
        var sw = new StringWriter();
        Console.SetOut(sw);
        var sr = new StringReader("X\nSELECTOR");
        Console.SetIn(sr);

        var m = new Menu("TITLE");
        m.AddOption(new Option("SELECTOR", "OPTION_TITLE", null));

        if (_application == null)
        {
            Assert.Fail();
        }
        else
        {
            m.Prompt(_application, _inventory, _basket);
            // Prompt, then error message, then prompt again for
            // the input of the correct selector
            Assert.That(sw.ToString(), Is.EqualTo("> \nInvalid option\n> "));
        }
    }
}