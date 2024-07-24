using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Configurations;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Contracts.Identity;
using Infrastructure.Identity;
using Serilog;
using Serilog.Events;
using CommonLogging;
using Microsoft.OpenApi.Models;

namespace Product.API.Extensions
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
        /// Add service/repository
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection ConfigureAddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();

            return services;

        }

        /// <summary>
        /// configure settings -> get data in file setting ... .json
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureAddConfigurationSettings(this IServiceCollection services,
        IConfiguration configuration)
        {
            // JwtSettings
            var jwtSettings = configuration.GetSection(nameof(JwtSettings))
                .Get<JwtSettings>();
            if (jwtSettings != null) services.AddSingleton(jwtSettings);

            // Database

            // ApiConfiguration
            var apiConfiguration = configuration.GetSection(nameof(ApiConfiguration))
                .Get<ApiConfiguration>();
            if (apiConfiguration != null) services.AddSingleton(apiConfiguration);

            return services;
        }
        /// <summary>
        /// Config Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureSwagger(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();

            // lay dia chi cua server: vd: http://localhost:5901/
            var configuration = configurations.GetSection("IdentityServer:BaseUrl").Value;

            if (string.IsNullOrEmpty(configuration))
            {
                var getConfigIdentityServer = services.GetOptions<ApiConfiguration>(nameof(ApiConfiguration));

                configuration = getConfigIdentityServer?.IdentityServerBaseUrl;
            }
            services.AddSwaggerGen(c =>
            {
                //c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sample Product API",
                    Version = "v1",
                    // Contact = new OpenApiContact
                    // {
                    //     Name = "Sample Product API",
                    //     Email = "gacdemxuan@gmail.com",
                    //     Url = new Uri("https://...com")
                    // }
                });


                // config authorization
                c.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{configuration}/connect/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "tedu_microservices_api.read", "IDP API Read your data (Scope)" },
                                { "tedu_microservices_api.write", "IDP API Write your data (Scope)" }
                            }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>
                        {
                            /// this list is scope list
                            "tedu_microservices_api.read",
                            "tedu_microservices_api.write"
                        }
                    }
                });
            });
        }

        /// <summary>
        /// configure Serilog -> dua vao elastic to trace later
        /// </summary>
        /// <param name="host"></param>
        /// <exception cref="Exception"></exception>
        public static void ConfigureSerilog(this ConfigureHostBuilder host, WebApplicationBuilder builder)
        {
            var configSettings = builder.Services.GetOptions<ConfigSettings>(nameof(ConfigSettings));

            if (configSettings != null && !string.IsNullOrEmpty(configSettings?.ToString()) && configSettings.WriteElastic)
            {
                builder.Host.UseSerilog(Serilogger.Configure);
            }
            else if (configSettings != null && !string.IsNullOrEmpty(configSettings?.ToString()) && configSettings.WriteLogsFile)
            {
                host.UseSerilog((context, configuration) =>
                {
                    var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
                    var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

                    configuration
                        .WriteTo.Debug()
                        .WriteTo.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                        .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, shared: true)
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName!)
                        .Enrich.WithProperty("Application", applicationName!)
                        .ReadFrom.Configuration(context.Configuration)
                        ;

                });
            }
            else
            {
                host.UseSerilog((context, configuration) =>
                {
                    var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
                    var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

                    configuration
                        .WriteTo.Debug()
                        .WriteTo.Console(
                            outputTemplate:
                            "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                        //.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, shared: true)
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName!)
                        .Enrich.WithProperty("Application", applicationName!)
                        .ReadFrom.Configuration(context.Configuration)
                        ;
                });
            }
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

        /// <summary>
        /// Configure Jwt Authentication
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        internal static IServiceCollection ConfigureAddJwtAuthentication(this IServiceCollection services)
        {

            var settings = services.GetOptions<JwtSettings>(nameof(JwtSettings));
            if (settings == null || string.IsNullOrEmpty(settings.Key))
                throw new ArgumentNullException($"{nameof(JwtSettings)} is not configured properly");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = false
            };
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });

            return services;
        }


    }
}
