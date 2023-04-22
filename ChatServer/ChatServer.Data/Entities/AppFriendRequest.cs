using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppFriendRequest : AppEntityBase
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public int StatusId { get; set; }
		public MstStatusRequest StatusRequest { get; set; }
		public AppUser UserSendRequest { get; set; }
		public AppUser UserReceiveRequest { get; set; }
	}
}
