using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTeste.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ThrowController : ControllerBase
    {
        [Route("/error")]
        public IActionResult HandleError() => Problem();

        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment env)  {
            if (!env.IsDevelopment()) {
                return NotFound();
            }

            var exceptionHandlerFeatures = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: exceptionHandlerFeatures.Error.StackTrace,
                title: exceptionHandlerFeatures.Error.Message
            );
        }
    }
}
