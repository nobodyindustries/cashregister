using CashRegister.Management;

namespace TestCashRegister.TestProductManagement;

[TestFixture]
public class TestBasketItem
{
    [Test]
    public void TestBasketItemCalculatePrice()
    {
        var product = new Product("XX", "Product", 350);
        var basketItem = new BasketItem(product);
        var price = Convert.ToDecimal(3.50);
        
        Assert.That(basketItem.CalculatePrice(), Is.EqualTo(price));
    }

    [Test]
    public void TestBasketItemCalculatePriceAfterQuantityIncrement()
    {
        var product = new Product("XX", "Product", 350);
        var basketItem = new BasketItem(product);
        var price = Convert.ToDecimal(10.50);

        basketItem.Quantity += 2;
        
        Assert.That(basketItem.CalculatePrice(), Is.EqualTo(price));
    }
}