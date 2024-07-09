using Microsoft.AspNetCore.HttpOverrides;

namespace Ordering.API.Extensions
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
            builder.Services.ConfigureAddInfrastructure(builder.Configuration);

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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{app.Environment.ApplicationName} v1");
            });

            // uncomment if you want to add a UI
            app.UseStaticFiles();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();

            //set cookie policy before authentication/authorization setup
            app.UseCookiePolicy();

            app.UseConfigureEndpoints();


            return app;
        }

    }
}
