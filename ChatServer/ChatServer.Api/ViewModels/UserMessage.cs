using ChatServer.Data.Entities;

namespace ChatServer.Api.ViewModels
{
	public class UserMessage
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public AppMessage lastMesg { get; set; }
		public string Avatar { get; set; }
	}
}
