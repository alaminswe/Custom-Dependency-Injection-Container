namespace DependencyInjection.Container;

public class ServiceProvider
{
    private readonly List<ServiceDescriptor> _descriptors;
    private readonly HashSet<Type> _resolving = [];
    private readonly Dictionary<Type, object> _singletonInstances = [];

    public ServiceProvider(List<ServiceDescriptor> descriptors)
    {
        _descriptors = descriptors;
    }

    public T GetRequiredService<T>()
    {
        var result = GetService(typeof(T));
        if (result == null)
            throw new InvalidOperationException($"Service of type {typeof(T).Name} is not registered.");
        return (T)result;
    }

    public object? GetService(Type serviceType, Dictionary<Type, object>? scopedInstances = null)
    {
        var descriptor = _descriptors.FirstOrDefault(d => d.ServiceType == serviceType);

        if (descriptor == null)
            return null;

        return descriptor.Lifetime switch
        {
            ServiceLifetime.Singleton => GetOrCreateFromCache(_singletonInstances, descriptor, serviceType, scopedInstances),
            ServiceLifetime.Scoped => scopedInstances != null 
                ? GetOrCreateFromCache(scopedInstances, descriptor, serviceType, scopedInstances)
                : ResolveWithCycleCheck(descriptor, serviceType, scopedInstances),
            ServiceLifetime.Transient => ResolveWithCycleCheck(descriptor, serviceType, scopedInstances),
            _ => throw new NotSupportedException($"Unknown lifetime: {descriptor.Lifetime}")
        };
    }

    private object GetOrCreateFromCache( Dictionary<Type, object> cache, ServiceDescriptor descriptor, Type serviceType, Dictionary<Type, object>? scopedInstances)
    {
        if (cache.TryGetValue(serviceType, out var existingInstance))
        {
            return existingInstance;
        }

        var newInstance = ResolveWithCycleCheck(descriptor, serviceType, scopedInstances);
        cache[serviceType] = newInstance;
        return newInstance;
    }

    private object ResolveWithCycleCheck(ServiceDescriptor descriptor, Type serviceType, Dictionary<Type, object>? scopedInstances)
    {
        if (_resolving.Contains(serviceType))
        {
            throw new InvalidOperationException($"Circular dependency detected: {serviceType.Name}");
        }

        _resolving.Add(serviceType);
        try
        {
            return CreateInstance(descriptor.ImplementationType, scopedInstances);
        }
        finally
        {
            _resolving.Remove(serviceType);
        }
    }

    private object CreateInstance(Type implementationType, Dictionary<Type, object>? scopedInstances)
    {
        var constructor = implementationType.GetConstructors().First();
        var parameters = constructor.GetParameters();

        var resolvedParams = new object[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            Type paramType = parameters[i].ParameterType;
            
            var resolvedService = GetService(paramType, scopedInstances);

            if (resolvedService == null)
                throw new InvalidOperationException($"Cannot resolve dependency: {paramType.Name}");

            resolvedParams[i] = resolvedService;
        }

        return Activator.CreateInstance(implementationType, resolvedParams)!;
    }

    public ServiceScope CreateScope()
    {
        return new ServiceScope(this);
    }
}