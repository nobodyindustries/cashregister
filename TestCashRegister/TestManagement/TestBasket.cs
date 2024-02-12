using System.Reflection;
using CashRegister.Management;
using CashRegister.Management.RuleEngine;

namespace TestCashRegister.TestManagement;

[TestFixture]
public class TestBasket
{
    
    private readonly Product _product1 = new ("XX", "Product 1", 420);
    private readonly Product _product2 = new ("ZZ", "Product 2", 125);

    private readonly List<IBasketRule> _rules = GetRules();
    
    private static List<IBasketRule> GetRules()
    {
        // Dynamically load and instantiate all the rules
        var ruleInstances = 
            from t in Assembly.GetAssembly(typeof(IBasketRule))?.GetTypes()
            where t.GetInterfaces().Contains(typeof(IBasketRule))
                  && t.GetConstructor(Type.EmptyTypes) != null
            select Activator.CreateInstance(t) as IBasketRule;
        return ruleInstances.ToList();
    }
    
    [Test]
    public void TestBasketAddOneProductOnce()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        
        var totalItemCount = basket.Content.Sum(item => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content, Has.Count.EqualTo(1));
            Assert.That(totalItemCount, Is.EqualTo(1));
        });
    }

    [Test]
    public void TestBasketAddOneProductMoreThanOnce()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        basket.AddProduct(_product1);
        
        var totalItemCount = basket.Content.Sum(item => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content, Has.Count.EqualTo(1));
            Assert.That(totalItemCount, Is.EqualTo(2));
        });
    }
    
    [Test]
    public void TestBasketAddMoreThanOneProductOnce()
    {
        var basket = new Basket();
        basket.AddProduct(_product1);
        basket.AddProduct(_product2);
        
        var totalItemCount = basket.Content.Sum(item => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content, Has.Count.EqualTo(2));
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
        
        var totalItemCount = basket.Content.Sum(item => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content, Has.Count.EqualTo(2));
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
        
        var totalItemCount = basket.Content.Sum(item => item.Quantity);
        
        Assert.Multiple(() =>
        {
            Assert.That(basket.Content, Has.Count.EqualTo(0));
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

    [Test]
    public void TestBasketToStringProductsNoDiscounts()
    {
        var basket = new Basket();
        // One unit of "Product 1" should not trigger discounts
        basket.AddProduct(_product1);
        const string expected = "\n= INVOICE =\n\n- Products -\nProduct 1 (4.2€) x 1 = 4.2€\n\nTOTAL: 4.2€\n";
        Assert.That(basket.ToString(), Is.EqualTo(expected));
        
    }
    
    [Test]
    public void TestBasketToStringWithDiscounts()
    {
        var basket = new Basket();
        // Two units of "Product 1" should trigger the test discount
        basket.AddProduct(_product1);
        basket.AddProduct(_product1);
        const string expected = "\n= INVOICE =\n\n- Products -\nProduct 1 (4.2€) x 2 = 8.4€\n- Discounts -\nTest BasketRule for invalid product (No discount) -> 0.02€\n\nTOTAL: 8.42€\n";
        Assert.That(basket.ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void TestBasketReloadRulesOnConstruction()
    {
        var basket = new Basket();
        Assert.That(basket.DiscountRules, Is.EquivalentTo(_rules).Using(new BasketRuleComparer()));
    }

    [Test]
    public void TestBasketReloadRulesOnClear()
    {
        var basket = new Basket();
        basket.Clear();
        Assert.That(basket.DiscountRules, Is.EquivalentTo(_rules).Using(new BasketRuleComparer()));
    }
}