using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatServer.Api.Controllers
{
	[ApiController]
	public abstract class AppControllerBase : Controller
	{
		protected readonly IMapper _mapper;
		protected const string DEFAULT_FOLDER_AVATAR = "avatar";
		protected const string DEFAULT_AVATAR = "default_avatar.jpg";
		protected const string ERROR_NAME = "Có lỗi trong quá trình xử lý!";
		protected const short EXPIRED_VERIFY_CODE = 5;
		public AppControllerBase(IMapper mapper)
		{
			_mapper = mapper;
		}

	}
}
