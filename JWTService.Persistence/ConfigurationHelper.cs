using Microsoft.Extensions.Configuration;

namespace JWTService.Persistence
{
	public static class ConfigurationHelper
	{

		public static IConfiguration configuration;
		public static void Initialize(IConfiguration Configuration)
		{
			configuration = Configuration;
		}
	}

} 
