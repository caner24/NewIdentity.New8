using Microsoft.AspNetCore.Diagnostics;

namespace NewIdentity.ExceptionHandler
{
    public class GlobalHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not null)
            {
                httpContext.Response.StatusCode = exception switch
                {
                    ArgumentNullException => StatusCodes.Status400BadRequest,
                    _ => StatusCodes.Status500InternalServerError
                };
                var responses = new
                {
                    Error = $"{exception.Message}",
                    Code = httpContext.Response.StatusCode
                };
                await httpContext.Response.WriteAsJsonAsync(responses);
                return true;
            }
            return false;
        }
    }
}
