using System.Net;
using Shop.Domain.Abstractions;

namespace Shop.Domain.Exceptions;

public class NotFoundException : ExceptionBase
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}