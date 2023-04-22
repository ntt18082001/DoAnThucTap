﻿using AutoMapper;
using ChatServer.Api.Services;
using ChatServer.Api.Services.Interfaces;
using ChatServer.Api.ViewModels.User;
using ChatServer.Data.Entities;
using ChatServer.Data.Services;
using ChatServer.Shared.Consts;
using ChatServer.Shared.DTOs;
using ChatServer.Shared.DTOs.User;
using ChatServer.Shared.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace ChatServer.Api.Controllers
{
	[Route("[controller]")]
	[Authorize]
	public class AccountController : AppControllerBase
	{
		private readonly UserService _userService;
		private readonly IFileStorageService _fileStorageService;
		public AccountController(
			IMapper mapper,
			UserService userService,
			IFileStorageService fileStorageService) : base(mapper)
		{
			_userService = userService;
			_fileStorageService = fileStorageService;
		}
		[AllowAnonymous]
		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
		{
			if (!ModelState.IsValid)
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
			if (await _userService.CheckUserExist(x => x.Email == registerDTO.Email))
			{
				return BadRequest(new
				{
					Error = new
					{
						Email = "Email đã tồn tại!"
					}
				});
			}
			try
			{
				var hashResult = PwdHashHelper.HashHMACSHA512(registerDTO.Password);
				var user = _mapper.Map<AppUser>(registerDTO);
				user.PasswordHash = hashResult.Value;
				user.PasswordSalt = hashResult.Key;
				user.Username = registerDTO.Email;
				user.AppRoleId = RoleConst.UserRole.ID;
				user.Avatar = DEFAULT_FOLDER_AVATAR + "/" + DEFAULT_AVATAR;
				var isSuccess = await _userService.Register(user);
				return Ok(isSuccess);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
		[HttpPut("updateuser")]
		public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDTO updateUserDTO)
		{
			var user = await _userService.GetUser(updateUserDTO.Id);
			if(user == null)
			{
				return BadRequest("Không tìm thấy người dùng!");
			}
			try
			{
				user.FullName = updateUserDTO.FullName;
				user.PhoneNumber1 = updateUserDTO.PhoneNumber;
				user.Address = updateUserDTO.Address;
				var isSuccess = await _userService.UpdateProfileUser(user);
				return Ok(new
				{
					Id = user.Id,
					FullName = user.FullName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber1,
					Avatar = user.Avatar,
					Role = user.AppRole.Name,
					Address = user.Address
				});
			} catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPost("updateavatar")]
		public async Task<IActionResult> UpdateAvatar([FromForm] UpdateAvatarVM model)
		{
			var user = await _userService.GetUser(model.Id);
			if (user == null)
			{
				return BadRequest("Không tìm thấy người dùng!");
			}
			try
			{
				if (model.File != null)
				{
					var fileName = await _fileStorageService.SaveFile(DEFAULT_FOLDER_AVATAR, model.File);
					user.Avatar = DEFAULT_FOLDER_AVATAR + "/" + fileName;
				}
				var isSuccess = await _userService.UpdateProfileUser(user);
				if (isSuccess)
				{
					return Ok(new
					{
						Avatar = user.Avatar
					});
				}
				else
				{
					return BadRequest(ERROR_NAME);
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpPost("changepassword")]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
		{
			if (!ModelState.IsValid)
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
			var user = await _userService.GetUser(model.Id);
			if (user == null)
			{
				return BadRequest(ERROR_NAME);
			}
			try
			{
				var encryptPwd = PwdHashHelper.HashHMACSHA512WithKey(model.Password, user.PasswordSalt);
				if (!encryptPwd.SequenceEqual(user.PasswordHash))
				{
					return BadRequest(new
					{
						Error = new
						{
							Password = "Mật khẩu cũ không chính xác!"
						}
					});
				}
				var hashResult = PwdHashHelper.HashHMACSHA512(model.NewPassword);
				user.PasswordHash = hashResult.Value;
				user.PasswordSalt = hashResult.Key;
				var isSuccess = await _userService.UpdateUser(user);
				return Ok(isSuccess);
			} catch(Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
		[HttpGet("GetListFriend")]
		public async Task<IActionResult> GetListFriend()
		{
			try
			{
				var id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
				var result = await _userService.GetListFriend(Convert.ToInt32(id));
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
	}
}