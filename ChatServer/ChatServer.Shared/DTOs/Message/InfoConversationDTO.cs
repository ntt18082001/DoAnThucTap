using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class InfoConversationDTO
	{
		public int Id { get; set; }
		public int ConversationId { get; set; }
		public int? ColorId { get; set; }
		public string MainEmoji { get; set; }
		public ColorConversationDTO ColorConversation { get; set; }
	}
}
