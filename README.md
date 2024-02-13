# CashRegister

CashRegister is a program that allows to manage an inventory and generate invoices based on a list of products. The system allows as well to set rules to establish discounts (or penalties) based on the content of the basket that generates the invoice.

It is a CLI program that is invoked with no arguments and presents interactive menus that allow to execute the different actions that the program provides.

The IDE used throughout the development was Rider, from Jetbrains, and a shared configuration is part of this repository.

## Building

- The solution targets the .Net 8.0 platform, allowing rolling forward to the latest major version.
- The dependencies are managed with NuGet and configured as part of the solution. Currently depends on the following packages
  - Microsoft.Extensions.Logging
  - Microsoft.Extensions.Logging.Console
- In order to build the application, the preferred method is to open the solution in an IDE (JetBrains Rider, Visual Studio) and use the build command.
- The NUnit framework is used for testing. To run the tests its default test runner can be used on the _TestCashRegister_ project in the solution. There is a GitHub actions workflow that executes the tests every time a change is pushed to main.

## Structure

The code is structured based on functional units. There are two projects on the solution.
- *CashRegister* which is the application itself
- *TestCashRegister* which contains the tests for the main project.

The test project mirrors the structure of the application project, which is the following:

### Data

The **Data** folder contains the definition of the product data. In this case it is a CSV (comma-separated values) file that contains the products with the following fields for each product: 

`product_code,product_description,price_in_cents`

The file name is expected to be `products.csv`.

### Logger

The **Logger** folder contains a wrapper to a logger that outputs the entries to the console.

### Management

The **Management** folder contains the business logic of the application. The **Rule Engine** subfolder contains everything related to the rules that can apply to the invoices.

### User Interface

The **UserInterface** folder contains the classes that show information to the user. In this case the chosen output method has been the console, but in case this needed to be changed only code inside this folder would need to be changed.

## Common Tasks

### Add a new product

In order to add a new product you can just add a new line to the `products.csv` file with the specified format and relaunch the application.

### Remove a product

Remove the corresponding line on the `products.csv` file and restart the application. Remove the dead tests.

### Change the source of products

In order to change the origin of the product information to, for example, a database rather than a local CSV file, the only class that would need to be re-implemented is the `Inventory` class on the `Management` namespace. Some tests would need to change to reflect this change as well.

### Add a new discount rule

To add new discount rule we create a class that implements the `IBasketRule` interface on the `Management/RuleEngine/Rules` folder and add a new entry to the `BasketRuleIdentifiers` member of the interface.

This member is a dictionary that contains the types of rules and an integer, and it is used to determine rule equality. To keep things coherent, new rules are added at the end with consecutive indexes.

Remember to add the corresponding tests.

### Remove a discount rule

Delete the corresponding class and eliminate the entry of the `BasketRuleIdentifiers` member of the `IBasketRule` class.

### Change the output of the logger

At some point it might be desirable to redirect the output of the logger to a log sink or add other utilities like crash reporting. To do that, the only class that needs to be modified is `CashRegisterLogger`.

### Add or remove options or menus

The application CLI is built on the `UserInterface/Application` class. The Application keeps track of the menus by using an enumeration with their different IDs. Menus contain instances of Option that define how the selector (what the user needs to type to select that option), a description and a callback function that is executed when the option is selected.

The callback is a delegate that receives the whole app state, which includes the application itself, the product inventory and the current basket, so it can operate with any of them.

An option can move the application to a different menu by calling `SetCurrentMenu` and can exit by calling `Exit`, both part of the application instance.

The other menu options include basket operations, specifically add product and clear, which removes all added products.

View invoice shows an invoice for the current state of the basket, which includes a list of products, the applied discounts and the total.
