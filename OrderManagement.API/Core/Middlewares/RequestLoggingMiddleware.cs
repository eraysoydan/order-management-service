namespace OrderManagement.API.Core.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Request Received.");
                await _next(context);
            }
            finally
            {
                _logger.LogInformation("Request completed. Detail: {method} {url} => {statusCode}", context.Request?.Method, context.Request?.Path.Value, context.Response?.StatusCode);
            }
        }
    }
}
