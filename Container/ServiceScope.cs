namespace DependencyInjection.Container;

public class ServiceScope
{
    private readonly ServiceProvider _rootProvider;
    private readonly Dictionary<Type, object> _scopedInstances = [];
    public ServiceScope(ServiceProvider rootProvider)
    {
        _rootProvider = rootProvider;
    }

    public T GetRequiredService<T>()
    {
        var result = GetService(typeof(T));
        if (result == null)
            throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered.");
        return (T)result;
    }

    public object? GetService(Type serviceType)
    {
        return _rootProvider.GetService(serviceType, _scopedInstances);
    }
}