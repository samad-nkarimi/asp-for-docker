using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AspForDocker.AspDbContext;
using Microsoft.Extensions.Configuration;

Console.WriteLine("🚀 Migrator starting...");

var host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((ctx, config) =>
    {
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((ctx, services) =>
    {
        var conn = ctx.Configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(conn))
        {
            Console.WriteLine("❌ Connection string is empty or null.");
            Environment.Exit(1);  // stop early with clean exit
        }
        else
        {
            Console.WriteLine($"✅ Using connection string: {conn}");
        }
        Console.WriteLine("Using DB: " + conn);

        services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(conn));
    })
    .Build();

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

Console.WriteLine("✅ Migrations complete.");
