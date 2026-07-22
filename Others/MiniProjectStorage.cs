
namespace DependencyInjection.Others;
public interface IStorage
{
    public void SaveData();
}

public class FileStorage : IStorage
{
    public void SaveData()
    {
        Console.WriteLine("data saved");
    }
}

public class ReportGenerator
{
    private readonly IStorage _storage;
    public ReportGenerator(IStorage storage)
    {
        _storage = storage;
    }

    public void Report()
    {
        _storage.SaveData();
    }
}
