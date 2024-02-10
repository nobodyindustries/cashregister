using CashRegister.Navigation;

namespace TestCashRegister.TestNavigation;

[TestFixture]
public class TestOption
{
    [Test]
    public void TestOptionEmptyTitle()
    {
        Assert.Throws(typeof(ArgumentException), () => _ = new Option("Selector", ""));
    }
    
    [Test]
    public void TestOptionEmptySelector()
    {
        Assert.Throws(typeof(ArgumentException), () => _ = new Option("", "Title"));
    }

    [Test]
    public void TestOptionToString()
    {
        const string expected = "Selector - Title";
        var o = new Option("Selector", "Title");
        Assert.That(o.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestOptionEquals()
    {
        var o = new Option("S1", "Option 1");
        var oEqual = new Option("S1", "Option 1");
        var oEqualDifferentTitle = new Option("S1", "Option X");
        var oDifferent = new Option("S2", "Option 2");
        var oDifferentSameTitle = new Option("S2", "Option 1");
        
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
    public void TestProductHashCode()
    {
        var o = new Option("S1", "Option 1");
        var oEqual = new Option("S1", "Option 1");
        var oDifferent = new Option("S2", "Option 2");
        
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
}