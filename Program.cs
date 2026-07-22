using DependencyInjection.Container;
using DependencyInjection.Others;
using DependencyInjection.Practice;


var services = new CustomServiceCollection();
services.AddSingleton<ILogger, ConsoleLogger>();
services.AddScoped<IStorage, FileStorage>();
services.AddTransient<IEmailService, EmailService>();
services.AddTransient<NotificationService>();

var rootProvider = new ServiceProvider(services.GetDescriptors().ToList());

Console.WriteLine("=== Scope 1 ===");
using (var scope1 = rootProvider.CreateScope()!)
{
    var storage1 = scope1.GetRequiredService<IStorage>();
    var storage1Again = scope1.GetRequiredService<IStorage>();
    Console.WriteLine($"Same storage in Scope1: {ReferenceEquals(storage1, storage1Again)}"); // True
}

Console.WriteLine("=== Scope 2 ===");
using (var scope2 = rootProvider.CreateScope()!)
{
    var storage2 = scope2.GetRequiredService<IStorage>();
    Console.WriteLine($"Storage different across scopes: true (different memory) {storage2}");
}

Console.WriteLine("=== Singleton across everything ===");
var logger1 = rootProvider.GetRequiredService<ILogger>();
var logger2 = rootProvider.GetRequiredService<ILogger>();
Console.WriteLine($"Same logger always: {ReferenceEquals(logger1, logger2)}"); // True