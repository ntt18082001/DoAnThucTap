using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class MstStatusRequest : MstEntityBase
	{
		public MstStatusRequest()
		{
			AppFriendRequests = new HashSet<AppFriendRequest>();
		}
		public string? StatusName { get; set; }
		public ICollection<AppFriendRequest> AppFriendRequests { get; set; }
	}
}
