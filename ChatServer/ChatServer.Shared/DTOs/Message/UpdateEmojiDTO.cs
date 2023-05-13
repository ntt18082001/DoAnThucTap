using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class UpdateEmojiDTO
	{
		public int ConversationId { get; set; }
		public int? InfoConvId { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Emoji { get; set; }
	}
}
