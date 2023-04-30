namespace ChatServer.Api.ViewModels.Message
{
	public class GetMoreMessage
	{
		public int ConversationId { get; set; }
		public int LastMessageId { get; set; }
		public int LengthMessages { get; set; }
	}
}
