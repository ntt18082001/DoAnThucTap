using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppConversation : AppEntityBase
	{
		public AppConversation() 
		{
			Messages = new HashSet<AppMessage>();
		}
		public int UserId1 { get; set; }
		public int UserId2 { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public AppUser AppUser1 { get; set; }
		public AppUser AppUser2 { get; set; }
		public ICollection<AppMessage> Messages { get; set; }
	}
}
