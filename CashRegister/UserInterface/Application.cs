using CashRegister.Management;

namespace CashRegister.UserInterface;

public enum ApplicationMenuId
{
    ApplicationMenuMain = 0,
    ApplicationMenuAddProduct = 1
}

public class Application
{
    private readonly Dictionary<ApplicationMenuId, Menu> _menus = new();
    private ApplicationMenuId _currentMenuId = ApplicationMenuId.ApplicationMenuMain;
    private bool _isRunning = true;
    private readonly Inventory _inventory;
    private readonly Invoice _invoice;

    public Application(Inventory appInventory, Invoice appInvoice)
    {
        _inventory = appInventory;
        _invoice = appInvoice;
        
        // Create Main Menu
        var menu = new Menu("MAIN MENU");
        var option = new Option("A", "Add item(s) to invoice", (application, _, _) =>
        {
            application.SetCurrentMenu(ApplicationMenuId.ApplicationMenuAddProduct);
        });
        menu.AddOption(option);
        option = new Option("R", "Clear invoice", (_, _, invoice) =>
        {
            invoice.Clear();
        });
        menu.AddOption(option);
        option = new Option("F", "Apply offers, finalize and print invoice", (application, _, invoice) =>
        {
            invoice.ApplyDiscountRules();
            Console.Write(invoice.ToString());
            // Stays in the main menu after outputting the information
        });
        menu.AddOption(option);
        option = new Option("X", "Exit", (application, _, _) => application.Exit());
        menu.AddOption(option);
        _menus.Add(ApplicationMenuId.ApplicationMenuMain, menu);
        
        // Create Menu to add product to invoice
        menu = new Menu("ADD PRODUCT TO INVOICE");
        foreach (var p in _inventory.GetSortedProductList())
        {
            option = new Option(p.ProductCode, p.ProductName, (_, _, invoice) =>
            {
                invoice.AddProduct(p);
            });
            menu.AddOption(option);
        }
        option = new Option("B", "<- Back", (application, _, _) =>
        {
            application.SetCurrentMenu(ApplicationMenuId.ApplicationMenuMain);
        });
        menu.AddOption(option);
        _menus.Add(ApplicationMenuId.ApplicationMenuAddProduct, menu);
        
    }

    private void SetCurrentMenu(ApplicationMenuId menuId)
    {
        _currentMenuId = menuId;
    }

    private void Exit()
    {
        _isRunning = false;
    }

    public void Run()
    {
        while (_isRunning)
        {
            var currentMenu = _menus[_currentMenuId];
            Console.Write(currentMenu.ToString());
            currentMenu.Prompt(this, _inventory, _invoice);
        }
    }
}