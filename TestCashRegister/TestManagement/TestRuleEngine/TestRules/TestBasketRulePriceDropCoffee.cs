using CashRegister.Management;
using CashRegister.Management.RuleEngine.Rules;

namespace TestCashRegister.TestManagement.TestRuleEngine.TestRules;

[TestFixture]
public class TestBasketRulePriceDropCoffee
{
    private readonly Basket _basket = new();
    private readonly Inventory _inventory = new("./TestData/original_products.csv");
    private readonly BasketRulePriceDropCoffee _rule = new();

    [Test]
    public void TestGetDescription()
    {
        Assert.That(_rule.GetDescription(), Is.EqualTo("Coffee volume discount (VP of Eng. offer)"));
    }

    [Test]
    public void TestAppliesEmptyBasket()
    {
        _basket.Clear();
        Assert.That(_rule.Applies(_basket), Is.EqualTo(false));
    }
    
    [Test]
    public void TestAppliesNotEnoughProducts()
    {
        _basket.Clear();
        var product1 = _inventory.GetProductById("GR1");
        var product2 = _inventory.GetProductById("CF1");
        if(product1 == null || product2 == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product1);
            _basket.AddProduct(product2);
            _basket.AddProduct(product2);
            Assert.That(_rule.Applies(_basket), Is.EqualTo(false));
        }
    }
    
    [Test]
    public void TestAppliesMinimumProducts()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("CF1");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            Assert.That(_rule.Applies(_basket), Is.EqualTo(true));
        }
    }
    
    [Test]
    public void TestApplies()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("CF1");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            Assert.That(_rule.Applies(_basket), Is.EqualTo(true));
        }
    }
    
    [Test]
    public void TestGetAmountInCentsEmptyBasket()
    {
        _basket.Clear();
        Assert.That(_rule.GetAmountInCents(_basket), Is.EqualTo(0));
    }

    [Test]
    public void TestGetAmountInCentsDoesNotApply()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("CF1");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            Assert.That(_rule.GetAmountInCents(_basket), Is.EqualTo(0));
        }
    }
    
    [Test]
    public void TestGetAmountInCentsApplies()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("CF1");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            Assert.That(_rule.GetAmountInCents(_basket), Is.EqualTo(-1123));
            _basket.AddProduct(product);
            // The amount is calculated rounding to the nearest cent getting away from zero
            Assert.That(_rule.GetAmountInCents(_basket), Is.EqualTo(-1497));
        }
    }
}