using CashRegister.Management;
using CashRegister.UserInterface;

namespace TestCashRegister.TestNavigation;

[TestFixture]
public class TestOption
{
    private readonly OptionCallback _noopCallback = (_, _, _) => { };
    private readonly Navigation _navigation = new();
    private readonly Inventory _inventory = new("./TestData/valid_products.csv");
    private readonly Invoice _invoice = new();
    
    [Test]
    public void TestOptionEmptyTitle()
    {
        Assert.Throws(typeof(ArgumentException), () => _ = new Option("Selector", "", _noopCallback));
    }
    
    [Test]
    public void TestOptionEmptySelector()
    {
        Assert.Throws(typeof(ArgumentException), () => _ = new Option("", "Title", _noopCallback));
    }

    [Test]
    public void TestOptionToString()
    {
        const string expected = "Selector - Title";
        var o = new Option("Selector", "Title", _noopCallback);
        Assert.That(o.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestOptionEquals()
    {
        var o = new Option("S1", "Option 1", _noopCallback);
        var oEqual = new Option("S1", "Option 1", _noopCallback);
        var oEqualDifferentTitle = new Option("S1", "Option X", _noopCallback);
        var oDifferent = new Option("S2", "Option 2", _noopCallback);
        var oDifferentSameTitle = new Option("S2", "Option 1", _noopCallback);
        
        Assert.Multiple(() =>
        {
            Assert.That(o, Is.EqualTo(oEqual));
            Assert.That(o, Is.EqualTo(oEqualDifferentTitle));
            Assert.That(o, Is.Not.EqualTo(oDifferent));
            Assert.That(o, Is.Not.EqualTo(oDifferentSameTitle));
            Assert.That(o, Is.Not.EqualTo(null));
            #pragma warning disable NUnit2009
            Assert.That(o, Is.EqualTo(o));
            #pragma warning restore NUnit2009
        });
    }

    [Test]
    public void TestOptionHashCode()
    {
        var o = new Option("S1", "Option 1", _noopCallback);
        var oEqual = new Option("S1", "Option 1", _noopCallback);
        var oDifferent = new Option("S2", "Option 2", _noopCallback);
        
        Assert.Multiple(() =>
        {
            #pragma warning disable NUnit2009
            // The hash code needs to be deterministic (no usage of, for example, random)
            Assert.That(o.GetHashCode(), Is.EqualTo(o.GetHashCode()));
            #pragma warning restore NUnit2009
            Assert.That(o.GetHashCode(), Is.EqualTo(oEqual.GetHashCode()));
            Assert.That(o.GetHashCode(), Is.Not.EqualTo(oDifferent.GetHashCode()));
        });
    }

    [Test]
    public void TestExecute()
    {
        const string message = "Executed successfully";
        
        var sw = new StringWriter();
        Console.SetOut(sw);

        var o = new Option("Selector", "Title", (_, _, _) =>
        {
            Console.Write(message);
        });
        
        o.Execute(_navigation, _inventory, _invoice);

        var output = sw.ToString();
        Assert.That(output, Is.EqualTo(message));
    }

    [Test]
    public void TestExecuteNull()
    {
        var o = new Option("Selector", "Title", null);
        Assert.DoesNotThrow(() =>
        {
            o.Execute(_navigation, _inventory, _invoice);
        });
    }
}