using CashRegister.Inventory;

namespace TestCashRegister;

public class TestProduct
{
    [Test]
    public void TestProductToString()
    {
        var p = new Product("CF1", "Coffee", 123);
        const string expected = "Product -> Code: CF1 | Name: Coffee | PriceInCents: 123";
        Assert.That(p.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestProductEqualsNull()
    {
        var p = new Product("CF1", "Coffee", 123);
        Assert.That(p, Is.Not.EqualTo(null));
    }

    [Test]
    public void TestProductEquals()
    {
        var p = new Product("CF1", "Coffee", 123);
        var pEqual = new Product("CF1", "Coffee", 123);
        var pDifferentCode = new Product("CF2", "Coffee", 123);
        var pDifferentName = new Product("CF1", "Caffe Latte", 123);
        var pDifferentPrice = new Product("CF1", "Coffee", 456);
        
        Assert.Multiple(() =>
        {
            Assert.That(p, Is.EqualTo(pEqual));
            Assert.That(p, Is.Not.EqualTo(pDifferentCode));
            Assert.That(p, Is.Not.EqualTo(pDifferentName));
            Assert.That(p, Is.Not.EqualTo(pDifferentPrice));
        });
    }

    [Test]
    public void TestProductHashCode()
    {
        var p = new Product("CF1", "Coffee", 123);
        var pEqual = new Product("CF1", "Coffee", 123);
        var pDifferent = new Product("CF2", "Coffee", 123);

        Assert.Multiple(() =>
        {
            #pragma warning disable NUnit2009
            Assert.That(p.GetHashCode(), Is.EqualTo(p.GetHashCode()));
            #pragma warning restore NUnit2009
            Assert.That(p.GetHashCode(), Is.EqualTo(pEqual.GetHashCode()));
            Assert.That(p.GetHashCode(), Is.Not.EqualTo(pDifferent.GetHashCode()));
        });
    }
}