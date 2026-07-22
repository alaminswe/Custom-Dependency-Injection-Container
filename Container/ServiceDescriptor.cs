namespace DependencyInjection.Container;

public class ServiceDescriptor
{
    public Type ServiceType { get; }
    public Type ImplementationType { get; }
    public ServiceLifetime Lifetime { get; }

    public ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        ServiceType = serviceType;
        ImplementationType = implementationType;
        Lifetime = lifetime;
    }

    // Self-registration constructor (with out interface class)
    public ServiceDescriptor(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        ServiceType = serviceType;
        ImplementationType = serviceType;
        Lifetime = lifetime;
    }
}