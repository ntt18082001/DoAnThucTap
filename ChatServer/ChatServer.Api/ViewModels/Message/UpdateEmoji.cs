namespace ChatServer.Api.ViewModels.Message
{
	public class UpdateEmoji
	{
		public int ConversationId { get; set; }
		public int? InfoConvId { get; set; }
		public string Emoji { get; set; }
	}
}
