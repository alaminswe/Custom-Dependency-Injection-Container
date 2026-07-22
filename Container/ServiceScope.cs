namespace DependencyInjection.Container;

public class ServiceScope : IDisposable
{
    private readonly ServiceProvider _rootProvider;
    private readonly Dictionary<Type, object> _scopedInstances = new();
    private bool _disposed = false;

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
        if (_disposed)
            throw new ObjectDisposedException(nameof(ServiceScope), "Cannot resolve services from a disposed scope.");

        return _rootProvider.GetService(serviceType, _scopedInstances);
    }

    public void Dispose()
    {
        if (_disposed)
            return; 

        foreach (var instance in _scopedInstances.Values)
        {
            if (instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        _scopedInstances.Clear();
        _disposed = true;
    }
}