using AutoMapper;
using ChatServer.Api.Common.Attributes;
using ChatServer.Api.Services.Interfaces;
using ChatServer.Api.ViewModels.User;
using ChatServer.Data.Services;
using ChatServer.Shared.DTOs;
using ChatServer.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Api.Controllers
{
	[Route("[controller]")]
	public class LoginController : AppControllerBase
	{
		private readonly LoginService _loginService;
		private readonly ITokenService _tokenService;
		public LoginController(
			LoginService loginService,
			ITokenService tokenService,
			IMapper mapper) : base(mapper)
		{
			_loginService = loginService;
			_tokenService = tokenService;
		}
		[HttpGet("GetUser")]
		public async Task<IActionResult> Get()
		{
			var user = await _loginService.FindAccount("ntt180801@gmail.com");
			var userData = _mapper.Map<UserDataForApp>(user);
			return Ok(userData);
		}
		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginDTO model)
		{
			if(!ModelState.IsValid)
			{
				var errorMessages = ModelState.Values
											.SelectMany(x => x.Errors)
											.Select(x => x.ErrorMessage)
											.ToList();
				return BadRequest(new
				{
					Error = errorMessages
				});
			}
			var user = await _loginService.FindAccount(model.Email);
			if(user == null)
			{
				return BadRequest(new
				{
					Error = new
					{
						Email = "Tài khoản không tồn tại!"
					}
				});
			}
			var userData = _mapper.Map<UserDataForApp>(user);
			var pwdHash = PwdHashHelper.HashHMACSHA512WithKey(model.Password, userData.PasswordSalt);
			if (!pwdHash.SequenceEqual(user.PasswordHash))
			{
				return BadRequest(new
				{
					Error = new
					{
						Password = "Mật khẩu không chính xác!"
					}
				});
			}
			var token = _tokenService.GenerateToken(userData);
			return Ok(new
			{
				Token = token,
				User = new
				{
					Id = userData.Id,
					FullName = userData.FullName,
					Email = userData.Email,
					PhoneNumber = userData.PhoneNumber1,
					Avatar = userData.Avatar,
					Role = userData.RoleName,
					Address = userData.Address
				}
			});
		}
	}
}
