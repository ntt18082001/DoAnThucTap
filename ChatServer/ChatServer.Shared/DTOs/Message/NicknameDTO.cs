using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class NicknameDTO
	{
		public int Id { get; set; }
		public int ConversationId { get; set; }
		public int UserId { get; set; }
		public string Nickname { get; set; }
	}
}
