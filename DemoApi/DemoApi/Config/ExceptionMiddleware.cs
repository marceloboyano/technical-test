using System.Net;
using Newtonsoft.Json;


namespace DemoApi.Config
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// Middleware that intercepts and handles uncaught exceptions in the HTTP request pipeline.
        /// </summary>
        /// <param name="httpcontext"></param>
        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await _next(httpcontext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(httpcontext, ex);
            }
        }

        /// <summary>
        /// Handles exceptions by formatting a consistent error response and writing it to the HTTP response.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns>A task that represents the asynchronous write operation</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "An internal server error occurred. Please try again later.",
                Detailed = _env.IsDevelopment() ? exception.Message : null,
                StackTrace = _env.IsDevelopment() ? exception.StackTrace : null
            };

            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}

