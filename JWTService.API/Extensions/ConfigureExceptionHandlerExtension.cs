using Microsoft.AspNetCore.Diagnostics;
using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Serilog;

namespace JWTService.API.Extensions
{
	public static class ConfigureExceptionHandlerExtension
	{
		public static void ConfigureExceptionHandler(this WebApplication application)
		{
			application.UseExceptionHandler(builder =>
			{
				builder.Run(async context =>
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					context.Response.ContentType = MediaTypeNames.Application.Json;

					var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
					var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

					if (contextFeature != null)
					{
						Serilog.ILogger _log = Log.ForContext(typeof(Program));

						_log.Error($"Method : {exceptionHandlerPathFeature?.Path} - Message : {contextFeature.Error.Message}");

						await context.Response.WriteAsync(JsonSerializer.Serialize(new
						{
							StatusCode = context.Response.StatusCode,
							Message = "Beklenmedik Bir Hata İle karşılaşıldı",
							Title = "Hata alındı!"
						})); ;
					}
				});
			});
		}
	}
}
