using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.Api.Areas.Admin.Controllers
{
	public class ColorController : AdminControllerBase
	{
		public ColorController(IMapper mapper) : base(mapper)
		{
		}
		[HttpGet]
		public IActionResult Test()
		{
			return Ok("hihi");
		}
	}
}
