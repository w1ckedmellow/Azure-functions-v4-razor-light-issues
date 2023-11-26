using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using RazorLight;

namespace RazorFuncTest
{
    public class RenderTemplateDI
    {
        private readonly RazorLightEngine _engine;

        public RenderTemplateDI( RazorLightEngine engine )
        {
            _engine = engine;
        }

        [FunctionName( "RenderTemplateDI" )]
        public async Task<IActionResult> Run(
            [HttpTrigger( AuthorizationLevel.Function, "get", "post", Route = null )] HttpRequest req,
            ILogger log )
        {
            var model = new { Name = "John Doe", Time = DateTime.UtcNow };

            string result = await _engine.CompileRenderStringAsync( "templateKey", "Base", model );

            return new OkObjectResult( result );
        }
    }
}
