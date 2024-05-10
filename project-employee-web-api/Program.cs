using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using project_employee_web_api.DataContext;
using project_employee_web_api.Service.EmployeeService;
using project_employee_web_api.Service.UserService;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((hostContext, services) =>
                {
                    services.AddControllers();
                    services.AddEndpointsApiExplorer();
                    services.AddSwaggerGen();
                    services.AddCors(options =>
                    {
                        options.AddPolicy("CorsPolicy",
                            builder => builder
                                .AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                );
                    });
                    services.AddMemoryCache();
                    services.AddScoped<IEmployeeService, EmployeeService>();
                    services.AddScoped<IUserService, UserService>();
                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                    });
                });

                webBuilder.Configure((hostContext, app) =>
                {
                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        app.UseSwagger();
                        app.UseSwaggerUI();
                    }

                    // Habilitar o CORS
                    app.UseCors("CorsPolicy");

                    // app.UseHttpsRedirection();

                    app.UseAuthorization();

                    app.UseRouting();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });

                // Configurar para usar HTTP
                webBuilder.UseUrls("http://localhost:7289");
            });
}
