using CashRegister.Management;
using CashRegister.Management.RuleEngine.Rules;

namespace TestCashRegister.TestManagement.TestRuleEngine.TestRules;

[TestFixture]
public class TestBasketRuleTestImplementation
{
    private readonly Basket _basket = new();
    private readonly Inventory _inventory = new("./TestData/valid_products_xx_code.csv");
    private readonly BasketRuleTestImplementation _rule = new();

    [Test]
    public void TestGetDescription()
    {
        Assert.That(_rule.GetDescription(), Is.EqualTo("Test BasketRule for invalid product (No discount)"));
    }

    [Test]
    public void TestAppliesEmptyBasket()
    {
        Assert.That(_rule.Applies(_basket), Is.EqualTo(false));
    }
    
    [Test]
    public void TestAppliesNotEnoughProducts()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("XX");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            Assert.That(_rule.Applies(_basket), Is.EqualTo(false));
        }
    }
    
    [Test]
    public void TestAppliesMinimumProducts()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("XX");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            Assert.That(_rule.Applies(_basket), Is.EqualTo(true));
        }
    }
    
    [Test]
    public void TestApplies()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("XX");
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
    public void TestGetAmountInCentsDoesNotApply()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("XX");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            Assert.That(_rule.GetAmountInCents(_basket), Is.EqualTo(0));
        }
    }
    
    [Test]
    public void TestGetAmountInCentsApplies()
    {
        _basket.Clear();
        var product = _inventory.GetProductById("XX");
        if(product == null) Assert.Fail("Product not found on inventory");
        else
        {
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            Assert.That(_rule.GetAmountInCents(_basket), Is.EqualTo(2));
            _basket.AddProduct(product);
            _basket.AddProduct(product);
            Assert.That(_rule.GetAmountInCents(_basket), Is.EqualTo(4));
        }
    }
}