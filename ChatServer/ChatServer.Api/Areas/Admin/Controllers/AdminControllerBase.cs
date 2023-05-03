using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
		public AdminControllerBase(IMapper mapper)
		{
			_mapper = mapper;
		}
	}
}
