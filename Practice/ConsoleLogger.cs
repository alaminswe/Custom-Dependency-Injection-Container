namespace DependencyInjection.Practice;

public interface ILogger
{
    public void Print();
}

public class ConsoleLogger : ILogger
{
    public void Print()
    {
        Console.WriteLine("Printing From ConsoleLogger using ILogger Interface");
    }
}
public class LogMessagePrinter
{
    private readonly ILogger _logger;

    public LogMessagePrinter(ILogger logger)
    {
        _logger = logger;
    }

    public void log()
    {
        _logger.Print();
    }
}