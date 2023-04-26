namespace ChatServer.Api.ViewModels.Message
{
	public class SendMessage
	{
		public int UserId { get; set; }
		public int FriendId { get; set; }
		public string Content { get; set; }
		public IFormFile? file { get; set; }
	}
}
