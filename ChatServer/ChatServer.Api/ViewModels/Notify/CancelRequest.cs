namespace ChatServer.Api.ViewModels.Notify
{
	public class CancelRequest
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
	}
}
