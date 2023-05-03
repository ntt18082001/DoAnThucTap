using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.DTOs.Message
{
	public class InfoConversationDTO
	{
		public string UserNickname { get; set; }
		public string FriendNickname { get; set; }
		public string MainEmoji { get; set; }
	}
}
