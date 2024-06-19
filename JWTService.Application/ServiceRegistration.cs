using JWTService.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace JWTService.Application
{
	public static class ServiceRegistration
	{
		public static void AddApplicationServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(CreateTokenProfile));
		}
	}
}
