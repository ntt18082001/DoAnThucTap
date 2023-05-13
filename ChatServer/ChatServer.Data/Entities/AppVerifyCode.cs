using ChatServer.Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities
{
	public class AppVerifyCode : AppEntityBase
	{
		public string TokenString { get; set; }
		public DateTime Expired { get; set; }
		public bool IsVerified { get; set; }
		public int? UserId { get; set; }
		public AppUser AppUser { get; set; }
	}
}
