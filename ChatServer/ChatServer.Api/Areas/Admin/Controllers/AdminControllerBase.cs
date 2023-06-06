using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatServer.Api.Areas.Admin.Controllers
{
	[Area("Admin")]
	[ApiController]
	[Route("[area]/[controller]")]
	[Authorize(Roles = "Admin")]
	public class AdminControllerBase : Controller
	{
		protected readonly IMapper _mapper;
		protected const string DEFAULT_FOLDER_AVATAR = "avatar";
		protected const string DEFAULT_AVATAR = "default_avatar.jpg";
		protected const string ERROR_NAME = "Có lỗi trong quá trình xử lý!";
		protected const int DEFAULT_PAGE_SIZE = 10;
		protected int CurrentUserId { get => Convert.ToInt32(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)); }
		public AdminControllerBase(IMapper mapper)
		{
			_mapper = mapper;
		}
	}
}
