using CashRegister.Management;

namespace TestCashRegister;

[TestFixture]
public class TestProgram
{
    private readonly Basket _basket = new();
    private readonly Inventory _inventory = new("./TestData/original_products.csv");
    
    // These tests are the ones mentioned on the exercise definition
    [Test]
    public void TestDefinitionExample1()
    {
        _basket.Clear();
        var greenTea = _inventory.GetProductById("GR1");
        if (greenTea == null) Assert.Fail("Product not found");
        else
        {
            _basket.AddProduct(greenTea);
            _basket.AddProduct(greenTea);
            Assert.That(_basket.CalculateGrandTotal(), Is.EqualTo(new decimal(3.11)));
        }
    }
    
    [Test]
    public void TestDefinitionExample2()
    {
        _basket.Clear();
        var greenTea = _inventory.GetProductById("GR1");
        var strawberries = _inventory.GetProductById("SR1");
        if (greenTea == null || strawberries == null) Assert.Fail("Product not found");
        else
        {
            _basket.AddProduct(strawberries);
            _basket.AddProduct(strawberries);
            _basket.AddProduct(greenTea);
            _basket.AddProduct(strawberries);
            Assert.That(_basket.CalculateGrandTotal(), Is.EqualTo(new decimal(16.61)));
        }
    }
    
    [Test]
    public void TestDefinitionExample3()
    {
        _basket.Clear();
        var greenTea = _inventory.GetProductById("GR1");
        var strawberries = _inventory.GetProductById("SR1");
        var coffee = _inventory.GetProductById("CF1");
        if (greenTea == null || strawberries == null || coffee == null) Assert.Fail("Product not found");
        else
        {
            _basket.AddProduct(greenTea);
            _basket.AddProduct(coffee);
            _basket.AddProduct(strawberries);
            _basket.AddProduct(coffee);
            _basket.AddProduct(coffee);
            Assert.That(_basket.CalculateGrandTotal(), Is.EqualTo(new decimal(30.57)));
        }
    }
}