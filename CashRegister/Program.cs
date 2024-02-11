using CashRegister.Management;
using CashRegister.UserInterface;

var inventory = new Inventory("./Data/products.csv");
var basket = new Basket();

var application = new Application(inventory, basket);
application.Run();