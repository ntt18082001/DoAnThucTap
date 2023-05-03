using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class MessageDTO
	{
		public int Id { get; set; }
		public int ConversationId { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string? Content { get; set; }
		public string? UrlImage { get; set; }
		public bool IsLiked { get; set; }
		public bool IsSeen { get; set; }
		public bool IsDelete { get; set; }
		public DateTime SentTime { get; set; }
		public bool IsNotify { get; set; }
	}
}
