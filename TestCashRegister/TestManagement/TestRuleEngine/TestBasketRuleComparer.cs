using CashRegister.Management;
using CashRegister.Management.RuleEngine;
using CashRegister.Management.RuleEngine.Rules;

namespace TestCashRegister.TestManagement.TestRuleEngine;

[TestFixture]
public class TestBasketRuleComparer
{
    private readonly Basket _basket = new Basket();
    private readonly BasketRuleTestImplementation _testRule = new();
    private readonly IBasketRule? _differentRule;
    private readonly IBasketRule? _nullRule = null;

    public TestBasketRuleComparer()
    {
        
        var ruleList = _basket.DiscountRules.FindAll(rule => rule.GetType() != typeof(BasketRuleTestImplementation));
        if (ruleList.Count == 0)
        {
            _differentRule = null;
        }
        else
        {
            var rnd = new Random();
            _differentRule = ruleList[rnd.Next(ruleList.Count)];
        }   
    }
    
    [Test]
    public void TestEquals()
    {
        var testRule2 = (IBasketRule)new BasketRuleTestImplementation();
        var comparer = new BasketRuleComparer();
        if(_differentRule == null) Assert.Pass(); // Not enough implemented rules to test this
        else
        {
            Assert.Multiple(() =>
            {
                Assert.That(comparer.Equals(_nullRule, _nullRule), Is.EqualTo(true));
                Assert.That(comparer.Equals(_testRule, _nullRule), Is.EqualTo(false));
                Assert.That(comparer.Equals(_nullRule, _testRule), Is.EqualTo(false));
                Assert.That(_testRule, Is.Not.EqualTo(null).Using(new BasketRuleComparer()));
                Assert.That(_testRule, Is.EqualTo(testRule2).Using(new BasketRuleComparer()));
                Assert.That(_testRule, Is.Not.EqualTo(_differentRule).Using(new BasketRuleComparer()));
                Assert.That(_testRule.Equals(_nullRule), Is.EqualTo(false).Using(new BasketRuleComparer()));
                // ReSharper disable once EqualExpressionComparison
                Assert.That(_testRule.Equals(_testRule), Is.EqualTo(true).Using(new BasketRuleComparer()));
            });
        }
    }

    [Test]
    public void TestBasketRuleComparerGetHashcode()
    {
        var testRule2 = new BasketRuleTestImplementation();
        var comparer = new BasketRuleComparer();
        
        Assert.Multiple(() =>
        {
            #pragma warning disable NUnit2009
            // The hash code needs to be deterministic (no usage of, for example, random)
            Assert.That(comparer.GetHashCode(_testRule), Is.EqualTo(comparer.GetHashCode(_testRule)));
            #pragma warning restore NUnit2009
            // Same rule, different instance, still should be equal
            Assert.That(comparer.GetHashCode(_testRule), Is.EqualTo(comparer.GetHashCode(testRule2)));
            if(_differentRule != null) // If it is null there are not enough implemented rules to test this
            {
               Assert.That(comparer.GetHashCode(_testRule), Is.Not.EqualTo(comparer.GetHashCode(_differentRule)));
            }
        });
    }
}