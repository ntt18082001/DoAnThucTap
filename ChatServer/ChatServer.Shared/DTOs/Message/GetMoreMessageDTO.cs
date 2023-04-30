using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class GetMoreMessageDTO
	{
		public int ConversationId { get; set; }
		public bool CanGetMore { get; set; }
		public int LastMessageId { get; set; }
		public IEnumerable<MessageDTO> Messages { get;set; }
	}
}
