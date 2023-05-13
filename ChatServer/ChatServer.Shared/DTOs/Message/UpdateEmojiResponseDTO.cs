using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class UpdateEmojiResponseDTO
	{
		public int ConversationId { get; set; }
		public InfoConversationDTO InfoConversationDTO { get; set; }
		public MessageDTO NotifyMessage { get; set; }
	}
}
