using JWTService.Application.DTOs;

namespace JWTService.Application.Abstractions
{
	public interface ITokenService
	{
		Task<string> CreateToken(CreateTokenDTO dto);
	}
}
