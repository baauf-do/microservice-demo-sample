using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace CommonLogging
{
    public static partial class Serilogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
            (context, configuration) =>
            {
                var applicationName = context.HostingEnvironment.ApplicationName.ToLower().Replace(".", "-");
                var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";
                var elasticUri = context.Configuration.GetValue<string>(key: "ElasticConfiguration:Uri");
                var username = context.Configuration.GetValue<string>(key: "ElasticConfiguration:Username");
                var password = context.Configuration.GetValue<string>(key: "ElasticConfiguration:Password");

                configuration
                    .WriteTo.Debug()
                    .WriteTo.Console(
                        outputTemplate:
                        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri ?? string.Empty))
                    {
                        IndexFormat = $"logs-{applicationName}-{environmentName}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfReplicas = 1,
                        NumberOfShards = 2,
                        ModifyConnectionSettings = x => x.BasicAuthentication(username, password)
                    })
                    .Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .Enrich.WithProperty("Environment", environmentName)
                    .Enrich.WithProperty("Application", applicationName)
                    .ReadFrom.Configuration(context.Configuration);
            };
    }
}
