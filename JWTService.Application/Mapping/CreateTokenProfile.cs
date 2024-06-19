using AutoMapper;
using JWTService.Application.DTOs;
using JWTService.Application.Models;

namespace JWTService.Application.Mapping
{
	public class CreateTokenProfile:Profile
	{
		public CreateTokenProfile()
		{
			CreateMap<CreateTokenModel, CreateTokenDTO>().ReverseMap();
		}
	}
}
