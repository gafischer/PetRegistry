using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PetRegistry.API.Controllers.Base
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase, IActionFilter
    {
        private readonly ILogger _logger;
        protected readonly IMediator _mediator;

        public BaseController(ILogger logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.BeginScope($"RequestId:{context.HttpContext.TraceIdentifier}");
            _logger.LogInformation($"Endpoint {context.HttpContext.Request.Method} {context.HttpContext.Request.Path} started run.");
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                _logger.LogError(context.Exception, $"An error occured: {context.Exception.Message}");
                _logger.LogInformation($"Endpoint {context.HttpContext.Request.Method} {context.HttpContext.Request.Path} ended run with error.");
            }
            else
            {
                _logger.LogInformation($"Endpoint {context.HttpContext.Request.Method} {context.HttpContext.Request.Path} ended run.");
            }
        }
    }
}
