using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppNickname : AppEntityBase
	{
		public int ConversationId { get; set; }
		public int UserId { get; set; }
		public string Nickname { get; set; }
		public AppConversation AppConversation { get; set; }
	}
}
