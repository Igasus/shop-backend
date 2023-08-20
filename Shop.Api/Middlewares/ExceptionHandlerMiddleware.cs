using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Shop.Api.Common;
using Shop.Application;
using Shop.Domain.Abstractions;

namespace Shop.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public ExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception exception)
        {
            HandleException(exception, httpContext);
        }
    }

    private void HandleException(Exception exception, HttpContext httpContext)
    {
        HttpStatusCode statusCode;
        var responseBody = new ErrorResponseBody();
        
        switch (exception)
        {
            case ExceptionBase customException:
            {
                statusCode = customException.StatusCode;
                responseBody.Message = customException.Message;
                break;
            }
            default:
            {
                statusCode = HttpStatusCode.InternalServerError;
                responseBody.Message = _environment.IsDevelopment()
                    ? exception.Message
                    : ErrorMessages.InternalServerError;
                break;
            }
        }

        if (_environment.IsDevelopment())
        {
            responseBody.StackTrace = exception.StackTrace;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        httpContext.Response.WriteAsJsonAsync(responseBody);
    }
}