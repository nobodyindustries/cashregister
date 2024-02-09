using CashRegister.Inventory;

var i = new Inventory("./Data/products.csv");

Console.WriteLine(i.GetProductById("CF1")?.ToString());