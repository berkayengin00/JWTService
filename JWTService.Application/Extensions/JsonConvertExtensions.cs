using Newtonsoft.Json;


namespace JWTService.Application.Extensions
{
	public static class JsonConvertExtensions
	{
		public static T DeserializeAndHandleErrors<T>(string json, JsonConverter converter=null)
		{
			try
			{
				return JsonConvert.DeserializeObject<T>(json,converter);
			}
			catch (Exception ex)
			{
				//throw new Exception($"{ex.Message}");
				return default(T);
			}
		}

	}
}
