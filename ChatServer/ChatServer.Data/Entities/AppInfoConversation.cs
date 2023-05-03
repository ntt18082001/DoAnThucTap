using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppInfoConversation : AppEntityBase
	{
		public int ConversationId { get; set; }
		public int ColorId { get; set; }
		public string UserNickname { get; set; }
		public string FriendNickname { get; set; }
		public string MainEmoji { get; set; }
		public AppColorConversation AppColorConversation { get; set; }
		public AppConversation AppConversation { get; set; }
	}
}
