using DepartmentFunctions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DepartmentDbContext>(options =>
            options.UseSqlServer(
                context.Configuration["DepartmentDb"]));
    })
    .Build();

host.Run();
