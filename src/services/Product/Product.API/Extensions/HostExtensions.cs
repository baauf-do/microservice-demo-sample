using Infrastructure.Middlewares;
using Microsoft.AspNetCore.HttpOverrides;

namespace Product.API.Extensions
{
    internal static class HostExtensions
    {
        /// <summary>
        /// CONFIGURE
        ///
        /// add extension configure -> WebApplicationBuilder -> builder
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            // add configuration read file settings ... .json
            builder.ConfigureAddAppConfigurations();
            builder.Services.ConfigureAddConfigurationSettings(builder.Configuration);
            builder.Services.ConfigureAddInfrastructure(builder.Configuration);
            builder.Services.ConfigureAddInfrastructureServices();
            builder.Host.ConfigureSerilog(builder);

            builder.Services.ConfigureAddJwtAuthentication();
            // definition swagger
            builder.Services.ConfigureSwagger();

            return builder.Build();
        }

        /// <summary>
        /// USING (Configure Service Builder)
        ///
        /// add middle ware -> WebApplication -> app
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            // Config swagger
            app.UseSwaggerUI(c =>
            {
                var config = app.Configuration;
                c.OAuthClientId(config["ClientId"]);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{app.Environment.ApplicationName} v1");
                c.DisplayRequestDuration();
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseMiddleware<ErrorWrappingMiddleware>();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            //set cookie policy before authentication/authorization setup
            app.UseCookiePolicy();

            app.UseConfigureEndpoints();


            return app;
        }

    }
}
