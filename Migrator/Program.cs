using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using AspForDocker.AspDbContext;
using Microsoft.Extensions.Configuration;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(ctx.Configuration.GetConnectionString("DefaultConnection")));
    })
    .Build();

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
Console.WriteLine("Applying migrations...");
db.Database.Migrate();
Console.WriteLine("Done.");
