namespace Dependency_Injection__DI__Container.ThreeObjectClass;

public interface IEngine
{
    void Start();
}

public class Engine : IEngine
{
    public void Start()
    {
        Console.WriteLine("Engine Started");
    }
}