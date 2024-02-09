using CashRegister.Inventory;
using Microsoft.VisualBasic.FileIO;

namespace TestCashRegister;

public class TestInventory
{
    [Test]
    public void TestInventoryInvalidFilename()
    {
        Assert.Throws<FileNotFoundException>(() => _ = new Inventory("non_existing_file.csv"));
    }

    [Test]
    public void TestInventoryEmptyFile()
    {
        var i = new Inventory("./TestData/empty_products.csv");
        Assert.Multiple(() =>
        {
            Assert.That(i.ProductCount, Is.EqualTo(0));
            Assert.That(i.GetProductById("XXX"), Is.Null);
        });
    }

    [Test]
    public void TestInventoryInvalidPriceInCents()
    {
        Assert.Throws<MalformedLineException>(() => _ = new Inventory("./TestData/invalid_products_1.csv"));
    }
    
    [Test]
    public void TestInventoryInvalidNumberOfFields()
    {
        Assert.Throws<MalformedLineException>(() => _ = new Inventory("./TestData/invalid_products_2.csv"));
    }

    [Test]
    public void TestInventoryValidDatabase()
    {
        var i = new Inventory("./TestData/valid_products.csv");

        var product1 = new Product("CF1", "Café Especial", 123);
        var product2 = new Product("FD1", "Floppy Disk", 456);
        
        Assert.Multiple(() =>
        {
            Assert.That(i.ProductCount, Is.EqualTo(2));
            Assert.That(i.GetProductById("XXX"), Is.Null);
            Assert.That(i.GetProductById("CF1"), Is.EqualTo(product1));
            Assert.That(i.GetProductById("FD1"), Is.EqualTo(product2));
        });
    }
    
    [Test]
    public void TestInventoryValidDatabaseEmptyLine()
    {
        var i = new Inventory("./TestData/valid_products_empty_line.csv");

        var product1 = new Product("CF1", "Café Especial", 123);
        var product2 = new Product("FD1", "Floppy Disk", 456);
        
        Assert.Multiple(() =>
        {
            Assert.That(i.ProductCount, Is.EqualTo(2));
            Assert.That(i.GetProductById("XXX"), Is.Null);
            Assert.That(i.GetProductById("CF1"), Is.EqualTo(product1));
            Assert.That(i.GetProductById("FD1"), Is.EqualTo(product2));
        });
    }
    
}