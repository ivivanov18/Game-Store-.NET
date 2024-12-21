namespace GameStore.Api.Exceptions;

public static class CustomExceptionsHandler
{
	public static void AddCustomExceptionHandling(this WebApplication app)
	{
		app.Use(async (context, next) =>
		{
			try
			{
				await next();
			}
			catch(BadHttpRequestException ex)
			{
				context.Response.StatusCode = StatusCodes.Status400BadRequest;
				await context.Response.WriteAsJsonAsync(new
				{
					Error = "Invalid request body",
     		        Details = ex.Message
				});
			}
			catch(Exception ex)
			{
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				await context.Response.WriteAsJsonAsync(new
				{
					Error = "An unexpected error occurred.",
					Details = ex.Message
				});
			}
		});
	}
}
