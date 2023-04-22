namespace ChatServer.Api.ViewModels.User
{
	public class UpdateAvatarVM
	{
		public int Id { get; set; }
		public IFormFile File { get; set; }
	}
}
