using CashRegister.Management;
using CashRegister.UserInterface;

namespace TestCashRegister.TestUserInterface;

[TestFixture]
public class TestOption
{
    public static readonly OptionCallback NoopCallback = (_, _, _) => { };
    private readonly Inventory _inventory;
    private readonly Invoice _invoice;
    private readonly Application? _application;
    
    public TestOption()
    {
        _inventory = new Inventory("./TestData/valid_products.csv");
        _invoice = new Invoice();
        _application = new Application(_inventory, _invoice);
    }
    
    [Test]
    public void TestOptionEmptyTitle()
    {
        Assert.Throws(typeof(ArgumentException), () => _ = new Option("Selector", "", NoopCallback));
    }
    
    [Test]
    public void TestOptionEmptySelector()
    {
        Assert.Throws(typeof(ArgumentException), () => _ = new Option("", "Title", NoopCallback));
    }

    [Test]
    public void TestOptionToString()
    {
        const string expected = "Selector - Title";
        var o = new Option("Selector", "Title", NoopCallback);
        Assert.That(o.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestOptionEquals()
    {
        var o = new Option("S1", "Option 1", NoopCallback);
        var oEqual = new Option("S1", "Option 1", NoopCallback);
        var oEqualDifferentTitle = new Option("S1", "Option X", NoopCallback);
        var oDifferent = new Option("S2", "Option 2", NoopCallback);
        var oDifferentSameTitle = new Option("S2", "Option 1", NoopCallback);
        
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
        var o = new Option("S1", "Option 1", NoopCallback);
        var oEqual = new Option("S1", "Option 1", NoopCallback);
        var oDifferent = new Option("S2", "Option 2", NoopCallback);
        
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

        if (_application == null)
        {
            Assert.Fail();
        }
        else
        {
            o.Execute(_application, _inventory, _invoice);

            var output = sw.ToString();
            Assert.That(output, Is.EqualTo(message));    
        }
    }

    [Test]
    public void TestExecuteNull()
    {
        var o = new Option("Selector", "Title", null);
        if (_application == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.DoesNotThrow(() =>
            {
            
                o.Execute(_application, _inventory, _invoice);
            });
        }
    }
}