using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Consts
{
	public static class RoleConst
	{
		public static class AdminRole
		{
			public const short ID = 1;
			public const string NAME = "Admin";
			public const string DESC = "Quản trị hệ thống";
		}
		public static class UserRole
		{
			public const short ID = 2;
			public const string NAME = "User";
			public const string DESC = "Người dùng trong hệ thống";
		}
	}
}
