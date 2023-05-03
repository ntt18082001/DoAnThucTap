using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppColorConversation : AppEntityBase
	{
		public AppColorConversation()
		{
			AppInfoConversations = new HashSet<AppInfoConversation>();
		}
		public string BackgroundColorCode { get; set; }
		public string TextColorCode { get; set; }
		public ICollection<AppInfoConversation> AppInfoConversations { get; set; }
	}
}
