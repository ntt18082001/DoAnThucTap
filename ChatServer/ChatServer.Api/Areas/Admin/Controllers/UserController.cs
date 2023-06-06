using AutoMapper;
using ChatServer.Api.Areas.Admin.ViewModels.User;
using ChatServer.Api.Controllers;
using ChatServer.Data.Services;
using ChatServer.Shared.DTOs.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Api.Areas.Admin.Controllers
{
	[Authorize(Roles = "Admin")]
	public class UserController : AdminControllerBase
	{
		private readonly UserService _userService;
		public UserController(IMapper mapper, UserService userService) : base(mapper)
		{
			_userService = userService;
		}
		[HttpGet("GetAllUser")]
		public async Task<IActionResult> GetAllUser([FromQuery] SearchUserDTO search, int page = 1, int pageSize = 5)
		{
			try
			{
				var users = await _userService.GetAllUser(search, CurrentUserId, page, pageSize);
				return Ok(new PagedUser
				{
					Data = users,
					Page = users.PageNumber,
					Size = users.PageSize,
					TotalPages = users.PageCount,
					TotalItems = users.TotalItemCount
				});
			}
			catch (Exception ex)
			{
				return BadRequest(ERROR_NAME);
			}
		}
	}
}
