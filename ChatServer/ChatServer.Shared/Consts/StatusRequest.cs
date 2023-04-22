using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Consts
{
	public static class StatusRequest
	{
		public static class PendingRequest
		{
			public const short ID = 1;
			public const string NAME = "Đang chờ";
		}
		public static class AcceptedRequest
		{
			public const short ID = 2;
			public const string NAME = "Đã chấp nhận";
		}
		public static class RejectedRequest
		{
			public const short ID = 3;
			public const string NAME = "Đã từ chối";
		}
	}
}
