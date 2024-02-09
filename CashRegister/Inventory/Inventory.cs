using System.Data;
using CashRegister.Logger;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;

namespace CashRegister.Inventory;

public sealed class Inventory
{
    private readonly Dictionary<string, Product> _database;
    public int ProductCount { get; }

    public Inventory(string databaseFileName)
    {
        _database = new Dictionary<string, Product>();
        ProductCount = 0;

        using var reader = new TextFieldParser(databaseFileName);
        {
            reader.SetDelimiters([","]);
            while (!reader.EndOfData)
            {
                ProductCount++;
                var currentRow = reader.ReadFields();
                if (currentRow == null) continue;
                if (currentRow.Length != 3)
                {
                    _database.Clear();
                    new CashRegisterLogger().Log(LogLevel.Critical,
                        $"Error parsing database on line {ProductCount} [Not enough fields]");
                    throw new MalformedLineException();
                }

                int priceInCents;

                try
                {
                    priceInCents = int.Parse(currentRow[2]);
                }
                catch (Exception ex) when (ex is ArgumentNullException or FormatException or OverflowException)
                {
                    _database.Clear();
                    new CashRegisterLogger().Log(LogLevel.Critical,
                        $"Error parsing database on line {ProductCount} [Invalid field: PriceInCents (3)]");
                    throw new MalformedLineException();
                }

                _database.Add(currentRow[0], new Product(currentRow[0], currentRow[1], priceInCents));
            }
        }
    }

    public Product? GetProductById(string id)
    {
        _database.TryGetValue(id, out var p);
        return p;
    }
}