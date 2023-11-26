using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace RazorFuncTest
{
    public class RenderTemplate
    {
        [FunctionName( "RenderTemplate" )]
        public async Task<IActionResult> Run(
            [HttpTrigger( AuthorizationLevel.Function, "get", "post", Route = null )] HttpRequest req,
            ILogger log )
        {

            var _templatesPath = Path.Combine( Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location ), "..", "Templates" );

            var engine = new RazorLightEngineBuilder()
                     .UseFileSystemProject( _templatesPath )
                     //.SetOperatingAssembly( Assembly.GetExecutingAssembly() )
                     //.UseEmbeddedResourcesProject( typeof( NotificationService ) )
                     .UseMemoryCachingProvider()
                     .Build();


            var model = new { Name = "John Doe", Time = DateTime.UtcNow };

            string result = await engine.CompileRenderStringAsync( "templateKey", "Base", model );

            return new OkObjectResult( result );
        }
    }
}
