using System.Diagnostics;

namespace GameStore.Api.Middlewares;

public class RequestTimingMiddleware
(
	RequestDelegate next,
	ILogger<RequestTimingMiddleware> logger
) 
{
	public async Task InvokeAsync(HttpContext context)
	{
		var stopWatch = new Stopwatch();

		try
		{
			stopWatch.Start();

			await next(context);
		}
		finally
		{
			stopWatch.Stop();
			var ellapsedMilliseconds = stopWatch.ElapsedMilliseconds;
			logger.LogInformation("{RequestMethod} {RequestPath} completed with status {Status} in {ElapsedMilliseconds}ms",
						context.Request.Method,
						context.Request.Path,
						context.Response.StatusCode,
						ellapsedMilliseconds);
		}

	}
}