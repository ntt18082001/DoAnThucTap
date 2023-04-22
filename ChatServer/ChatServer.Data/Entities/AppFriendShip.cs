using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppFriendShip : AppEntityBase
	{
		public int UserId1 { get; set; } // UserId
		public int UserId2 { get; set; } // FriendId
		public AppUser AppUser1 { get; set; }
		public AppUser AppUser2 { get; set; }
	}
}
