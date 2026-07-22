using DependencyInjection.Container;
using DependencyInjection.Others;
using DependencyInjection.Practice;

var services = new CustomServiceCollection();
services.AddSingleton<ILogger, ConsoleLogger>();
services.AddScoped<IStorage, FileStorage>();
services.AddTransient<IEmailService, EmailService>();

var rootProvider = new ServiceProvider(services.GetDescriptors().ToList());

// ---- Scope 1 ----
var scope1 = rootProvider.CreateScope();
var storage1a = scope1.GetRequiredService<IStorage>();
var storage1b = scope1.GetRequiredService<IStorage>();
Console.WriteLine(ReferenceEquals(storage1a, storage1b)); // True — same Scope, same object

// ---- Scope 2  ----
var scope2 = rootProvider.CreateScope();
var storage2 = scope2.GetRequiredService<IStorage>();
Console.WriteLine(ReferenceEquals(storage1a, storage2)); // False — diff Scope, diff object

// ---- Singleton all Scope same  ----
var logger1 = scope1.GetRequiredService<ILogger>();
var logger2 = scope2.GetRequiredService<ILogger>();
Console.WriteLine(ReferenceEquals(logger1, logger2)); // True — Singleton, root shared