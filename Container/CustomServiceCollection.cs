namespace DependencyInjection.Container;

public class CustomServiceCollection
{
    private readonly List<ServiceDescriptor> _descriptors = [];

    // ---------- Transient ----------
    public void AddTransient<TService, TImplementation>()
        where TImplementation : TService
    {
        Add<TService, TImplementation>(ServiceLifetime.Transient);
    }

    public void AddTransient<TService>()
    {
        Add<TService>(ServiceLifetime.Transient);
    }

    // ---------- Scoped ----------
    public void AddScoped<TService, TImplementation>()
        where TImplementation : TService
    {
        Add<TService, TImplementation>(ServiceLifetime.Scoped);
    }

    public void AddScoped<TService>()
    {
        Add<TService>(ServiceLifetime.Scoped);
    }

    // ---------- Singleton ----------
    public void AddSingleton<TService, TImplementation>()
        where TImplementation : TService
    {
        Add<TService, TImplementation>(ServiceLifetime.Singleton);
    }

    public void AddSingleton<TService>()
    {
        Add<TService>(ServiceLifetime.Singleton);
    }

    // ------------ Remove methods------------

    public void Remove<TService>()
    {
        _descriptors.RemoveAll(d => d.ServiceType == typeof(TService));
    }

    public int Count => _descriptors.Count;



    // ---------- Private Helper DRY Principle (Don't Repeat Yourself) ----------
    private void Add<TService, TImplementation>(ServiceLifetime lifetime)
        where TImplementation : TService
    {
        _descriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime));
    }

    private void Add<TService>(ServiceLifetime lifetime)
    {
        _descriptors.Add(new ServiceDescriptor(typeof(TService), lifetime));
    }



    // পরবর্তী part এ এইখান থেকে ServiceProvider বানাবো
    public IReadOnlyList<ServiceDescriptor> GetDescriptors() => _descriptors;
}