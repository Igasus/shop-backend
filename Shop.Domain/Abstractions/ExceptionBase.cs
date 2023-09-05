using System;
using System.Net;

namespace Shop.Domain.Abstractions;

public abstract class ExceptionBase : Exception
{
    public HttpStatusCode StatusCode { get; }
    
    protected ExceptionBase(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}