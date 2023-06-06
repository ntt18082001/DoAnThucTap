using ChatServer.Shared.DTOs.User;

namespace ChatServer.Api.Areas.Admin.ViewModels.User
{
	public class PagedUser
	{
		public IEnumerable<UserDTO> Data { get; set; }
		public int Page { get; set; }
		public int Size { get; set; }
		public int TotalPages { get; set; }
		public int TotalItems { get; set; }
	}
}
