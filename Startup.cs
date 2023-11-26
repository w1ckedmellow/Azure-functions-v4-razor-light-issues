using System;
using System.IO;
using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RazorLight;

[assembly: FunctionsStartup( typeof( Functions.Startup ) )]
namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public const string ReportsClientName = "ReportsFunctions";

        public override void Configure( IFunctionsHostBuilder builder )
        {
            IConfigurationRoot config = new ConfigurationBuilder()
              .SetBasePath( Environment.CurrentDirectory )
              //.AddJsonFile( "local.settings.json" )
              .AddEnvironmentVariables()
              .Build();


            var _templatesPath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), "..", "Templates" );

            var engine = new RazorLightEngineBuilder()
                     .UseFileSystemProject( _templatesPath )
                     //.SetOperatingAssembly( Assembly.GetExecutingAssembly() )
                     //.UseEmbeddedResourcesProject( typeof( NotificationService ) )
                     .UseMemoryCachingProvider()
                     .Build();


            builder.Services.AddSingleton<RazorLightEngine>( engine );
        }
    }
}