using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoWebAPI.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error(
            [FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exceptionType = context.Error.GetType();

            _logger.LogError(context.Error, context.Error.Message);

            if (webHostEnvironment.IsDevelopment())
            {
                return Problem(
                    context.Error.StackTrace,
                    title: context.Error.Message,
                    statusCode: 500
                    );
            }

            return Problem();
        }
    }
}
