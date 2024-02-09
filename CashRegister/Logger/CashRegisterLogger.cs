using Microsoft.Extensions.Logging;

namespace CashRegister.Logger;

public class CashRegisterLogger
{
    private readonly ILogger _logger;

    public CashRegisterLogger()
    {
        // Simple console logging
        var loggerFactory = LoggerFactory.Create(builder => builder.AddSimpleConsole());
        _logger = loggerFactory.CreateLogger("CashRegister");
    }
    
    public void Log(LogLevel level, string message)
    {
        var date = DateTime.Now;
        _logger.Log(level, "[CashRegister] - {date:s} - {message}", date, message);
    }

}