using CashRegister.Management;

namespace TestCashRegister.TestProductManagement;

[TestFixture]
public class TestBasket
{
    
    private readonly Product _product1 = new ("XX", "Product 1", 420);
    private readonly Product _product2 = new ("ZZ", "Product 2", 125);

    
    [Test]
    public void TestBasketAddOneProductOnce()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        
        var totalItemCount = basket.Content.Sum((item) => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content.Count, Is.EqualTo(1));
            Assert.That(totalItemCount, Is.EqualTo(1));
        });
    }

    [Test]
    public void TestBasketAddOneProductMoreThanOnce()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        basket.AddProduct(_product1);
        
        var totalItemCount = basket.Content.Sum((item) => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content.Count, Is.EqualTo(1));
            Assert.That(totalItemCount, Is.EqualTo(2));
        });
    }
    
    [Test]
    public void TestBasketAddMoreThanOneProductOnce()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        basket.AddProduct(_product2);
        
        var totalItemCount = basket.Content.Sum((item) => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content.Count, Is.EqualTo(2));
            Assert.That(totalItemCount, Is.EqualTo(2));
        });
    }
    
    [Test]
    public void TestBasketAddMoreThanOneProductMoreThanOnce()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        basket.AddProduct(_product1);
        basket.AddProduct(_product2);
        basket.AddProduct(_product2);
        basket.AddProduct(_product2);
        
        var totalItemCount = basket.Content.Sum((item) => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content.Count, Is.EqualTo(2));
            Assert.That(totalItemCount, Is.EqualTo(5));
        });
    }

    [Test]
    public void TestBasketClear()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        basket.AddProduct(_product1);
        basket.AddProduct(_product2);
        
        basket.Clear();
        
        var totalItemCount = basket.Content.Sum((item) => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content.Count, Is.EqualTo(0));
            Assert.That(totalItemCount, Is.EqualTo(0));
        });
    }

    [Test]
    public void TestBasketToStringEmptyBasket()
    {
        var basket = new Basket();
        const string expected = "\nEmpty basket. Add at least a product to create an invoice\n";

        Assert.That(basket.ToString(), Is.EqualTo(expected));
    }
    
    // TODO: Test ToString when Discounts are developed
    [Test]
    public void TestBasketToString()
    {
        Assert.Pass();
    }
}