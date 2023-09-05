namespace Shop.Api.Common;

public record ErrorResponseBody
{
    public string Message { get; set; }
    public string StackTrace { get; set; }
}