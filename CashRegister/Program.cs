using CashRegister.Management;
using CashRegister.UserInterface;

var inventory = new Inventory("./Data/products.csv");
var invoice = new Basket();

var application = new Application(inventory, invoice);
application.Run();