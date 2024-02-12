using CashRegister.Management;

namespace TestCashRegister.TestManagement;

[TestFixture]
public class TestProduct
{
    private readonly Product _product = new("CF1", "Coffee", 123);
    
    [Test]
    public void TestProductToString()
    {
        const string expected = "Product -> Code: CF1 | Name: Coffee | PriceInCents: 123";
        Assert.That(_product.ToString(), Is.EqualTo(expected));
    }
    

    [Test]
    public void TestProductEquals()
    {
        var pEqual = new Product("CF1", "Coffee", 123);
        var pDifferentCode = new Product("CF2", "Coffee", 123);
        var pDifferentName = new Product("CF1", "Caffe Latte", 123);
        var pDifferentPrice = new Product("CF1", "Coffee", 456);
        Product? pNull = null;
        
        Assert.Multiple(() =>
        {
            Assert.That(_product, Is.EqualTo(pEqual));
            Assert.That(_product, Is.Not.EqualTo(pDifferentCode));
            Assert.That(_product, Is.Not.EqualTo(pDifferentName));
            Assert.That(_product, Is.Not.EqualTo(pDifferentPrice));
            Assert.That(_product.Equals(pNull), Is.EqualTo(false));
            // ReSharper disable once EqualExpressionComparison
            Assert.That(_product.Equals(_product), Is.EqualTo(true));
        });
    }

    [Test]
    public void TestProductHashCode()
    {
        var pEqual = new Product("CF1", "Coffee", 123);
        var pDifferent = new Product("CF2", "Coffee", 123);

        Assert.Multiple(() =>
        {
            #pragma warning disable NUnit2009
            // The hash code needs to be deterministic (no usage of, for example, random)
            Assert.That(_product.GetHashCode(), Is.EqualTo(_product.GetHashCode()));
            #pragma warning restore NUnit2009
            Assert.That(_product.GetHashCode(), Is.EqualTo(pEqual.GetHashCode()));
            Assert.That(_product.GetHashCode(), Is.Not.EqualTo(pDifferent.GetHashCode()));
        });
    }
    
}