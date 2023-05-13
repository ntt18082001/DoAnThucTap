using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message.Nickname
{
	public class UpdateNicknameResponseDTO
	{
		public int ConversationId { get; set; }
		public NicknameDTO Nickname { get; set; }
		public MessageDTO NotifyMessage { get; set; }
	}
}
