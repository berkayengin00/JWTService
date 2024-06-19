using AutoMapper;
using JWTService.Application.Abstractions;
using JWTService.Application.DTOs;
using JWTService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public TokenController(ITokenService tokenService, IMapper mapper)
		{
			_tokenService = tokenService;
			_mapper = mapper;
		}

		[HttpPost("CreateToken")]
		public async Task<string> CreateToken(CreateTokenModel model)
		{
			var dto = _mapper.Map<CreateTokenDTO>(model);
			return await _tokenService.CreateToken(dto);
		}
	}
}
