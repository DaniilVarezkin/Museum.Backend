using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace Museum.Application.Common.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger<TRequest> logger) =>
            _logger = logger;

        public async Task<TResponse> Handle(TRequest request, 
            RequestHandlerDelegate<TResponse> next, 
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var requestJson = JsonSerializer.Serialize(request);

            _logger.LogInformation("Museum Request: {Name}, {Request}",
                requestName, requestJson);

            var response = await next();

            return response;
        }
    }
}
