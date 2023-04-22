using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppMessage : AppEntityBase
	{
		public int ConversationId { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string? Content { get; set; }
		public string? UrlImage { get; set; }
		public bool IsLiked { get; set; }
		public DateTime SentTime { get; set; }
		public AppConversation Conversation { get; set; }
		public AppUser Sender { get; set; }
		public AppUser Receiver { get; set; }
	}
}
