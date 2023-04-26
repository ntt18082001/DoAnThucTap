using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class ConversationDTO
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int FriendId { get; set; }
		public bool CanGetMore { get; set; }
		public UserMessageDTO Friend { get; set; }
		public UserMessageDTO User { get; set; }
		public IEnumerable<MessageDTO> Conversation { get; set; }
		public MessageDTO LastMessage { get; set; }
	}
}
