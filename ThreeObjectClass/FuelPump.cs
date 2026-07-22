namespace Dependency_Injection__DI__Container.ThreeObjectClass;

public interface IFuelPump
{
    void Pump();
}

public class FuelPump : IFuelPump
{
    public void Pump()
    {
        Console.WriteLine("Fuel Pump Working");
    }
}