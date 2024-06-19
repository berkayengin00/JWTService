using JWTService.Application.Abstractions;
using JWTService.Persistence.Concrete.Service;
using Microsoft.Extensions.DependencyInjection;

namespace JWTService.Persistence
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddScoped<ITokenService, TokenService>();
		}
	}
}
