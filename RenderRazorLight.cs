using System;
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
    public static class RenderRazorLight
    {
        [FunctionName( "RenderRazorLight" )]
        public static async Task<IActionResult> Run(
            [HttpTrigger( AuthorizationLevel.Function, "get", "post", Route = null )] HttpRequest req,
            ILogger log )
        {
            var templatePath = "Templates.Base.cshtml";

            var _razorLightEngine = new RazorLightEngineBuilder()
                    .SetOperatingAssembly( Assembly.GetExecutingAssembly() )
                    .UseEmbeddedResourcesProject( typeof( RenderRazorLight ) )
                    .UseMemoryCachingProvider()
                    .EnableDebugMode( true )
                    .Build();


            var templateModel = new BaseModel()
            {
                Name = "Arrocho",
                Time = DateTime.UtcNow,
            };


            var gg = await _razorLightEngine.CompileRenderAsync( templatePath, templateModel );

            return new OkResult();
        }
    }
}
