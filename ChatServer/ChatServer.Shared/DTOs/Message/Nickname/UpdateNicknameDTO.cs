using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message.Nickname
{
	public class UpdateNicknameDTO
	{
		public int ConversationId { get; set; }
		public int? NicknameId { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public int UserIdUpdated { get; set; }
		public string Nickname { get; set; }
	}
}
