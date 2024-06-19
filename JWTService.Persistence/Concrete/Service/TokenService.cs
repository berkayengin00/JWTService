using JWTService.Application;
using JWTService.Application.Abstractions;
using JWTService.Application.DTOs;
using JWTService.Application.Extensions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTService.Persistence.Concrete.Service
{
	public class TokenService : ITokenService
	{
		public async Task<string> CreateToken(CreateTokenDTO dto)
		{
			var claimList = new List<Claim>();
			try
			{
				var issure = ConfigurationHelper.configuration.GetSection("Token:Issuer").Value;
				var audience = ConfigurationHelper.configuration.GetSection("Token:Audience").Value;
				var configSecurityKey = ConfigurationHelper.configuration.GetSection("Token:SecurityKey").Value;

				if (!string.IsNullOrEmpty(dto?.ClaimModel))
				{
					claimList = JsonConvertExtensions.DeserializeAndHandleErrors<List<Claim>>(dto.ClaimModel,new ClaimConverter());
				}

				SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configSecurityKey));

				SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

				var expiration = DateTime.Now.AddMinutes(1);
				JwtSecurityToken securityToken = new JwtSecurityToken(
					issuer: issure,
					audience: audience,
					expires: expiration,
					notBefore: DateTime.Now,
					signingCredentials: signingCredentials,
					claims: claimList
				);

				JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

				var token = tokenHandler.WriteToken(securityToken);

				return await Task.FromResult(token);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}
	}
}
