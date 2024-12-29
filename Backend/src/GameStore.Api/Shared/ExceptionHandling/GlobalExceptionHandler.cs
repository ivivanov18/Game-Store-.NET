using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace GameStore.Api.Shared.ExceptionHandling;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
		HttpContext httpContext,
		Exception exception,
		CancellationToken cancellationToken)
    {
		var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

		logger.LogError(exception,
		"Could not process request on {Machine}. TraceId: {TraceId}",
		Environment.MachineName,
		traceId);

		await Results.Problem(
			title: "An error ocurred while processing your request.",
			statusCode: StatusCodes.Status500InternalServerError,
			extensions: new Dictionary<string, object?>
			{
				["traceId"] = traceId.ToString()
			}
		).ExecuteAsync(httpContext);

		return true;
    }
}
