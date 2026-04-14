using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstoqueApi.Exceptions;

namespace EstoqueApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleAsync(context, exception);
            }
        }

        private async Task HandleAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case AppException appException:

                    _logger.LogError(appException, "Erro tratado: {Message}", appException.Message);

                    context.Response.StatusCode = appException.StatusCode;

                    var response = new ErrorResponse
                    {
                        StatusCode = appException.StatusCode,
                        Title = appException.Title,
                        Message = appException.Message,
                        StackTrace = appException.StackTrace

                    };

                    await context.Response.WriteAsJsonAsync(response);
                    break;
                
                default:
                    _logger.LogError(exception, "Erro não tratado: {Message}", exception.Message);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var responseDefault = new ErrorResponse
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Title = "Erro interno do servidor",
                        Message = "Ocorreu um erro inesperado do servidor.",
                        StackTrace = exception.StackTrace
                    };

                    await context.Response.WriteAsJsonAsync(responseDefault);
                    break;
            }
        }
    }
}