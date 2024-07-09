using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Extensions
{
    public static class ServiceExtensions
    {

        /// <summary>
        /// Read all appsettings....json files
        /// </summary>
        /// <param name="builder"></param>
        public static void ConfigureAddAppConfigurations(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true, reloadOnChange: false)
                ;

        }

        /// <summary>
        /// Add Infrastructure for ServiceExtension
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureAddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
                config.Filters.Add(new ProducesAttribute("application/json", "text/plain", "text/json"));
            });

            services.AddControllersWithViews();

            // uncomment if you want to add a UI
            services.AddRazorPages();
            services.Configure<RouteOptions>(option => option.LowercaseUrls = true);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            return services;
        }

        /// <summary>
        /// Config Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Use Configure Endpoints
        /// </summary>
        /// <param name="app"></param>
        public static void UseConfigureEndpoints(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                                name: "default",
                                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();

            });
        }
    }
}
