using FluentValidation;
using Museum.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Museum.WebApi.Middleware
{
    public class CustomExceptionMiddleware
    {
        public RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next) =>
            _next = next; 

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;
            var request = context.Request;

            // Логируем базовую информацию о запросе
            Console.WriteLine($"[Error] Request: {request.Method} {request.Path}");
            Console.WriteLine($"[Error] Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}");

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    Console.WriteLine($"[Validation Error] Status: {code}");
                    Console.WriteLine($"Validation errors: {result}");
                    break;

                case NotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    result = JsonSerializer.Serialize(new { error = exception.Message });
                    Console.WriteLine($"[Not Found Error] Status: {code}");
                    Console.WriteLine($"Message: {exception.Message}");
                    break;

                case FileUploadException fileUploadException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(new { error = exception.Message });
                    Console.WriteLine($"[File Upload Error] Status: {code}");
                    Console.WriteLine($"Message: {exception.Message}");
                    if (exception.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {exception.InnerException.Message}");
                    }
                    break;

                default:
                    result = JsonSerializer.Serialize(new { error = exception.Message });
                    Console.WriteLine($"[Server Error] Status: {code}");
                    Console.WriteLine($"Message: {exception.Message}");
                    Console.WriteLine($"Stack Trace: {exception.StackTrace}"); // Важно для отладки
                    break;
            }

            // Логируем дополнительные параметры запроса
            if (request.QueryString.HasValue)
            {
                Console.WriteLine($"Query String: {request.QueryString}");
            }

            if (request.ContentLength > 0 && request.ContentType?.Contains("application/json") == true)
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, leaveOpen: true);
                var body = reader.ReadToEndAsync().GetAwaiter().GetResult();
                request.Body.Position = 0;
                Console.WriteLine($"Request Body: {body}");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
