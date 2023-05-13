using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Helpers
{
	public class VerifyCodeHelper
	{
		public static string CreateCode()
		{
			Random random = new Random();
			return Convert.ToString(random.Next(111111, 999999));
		}
		public static bool IsCodeExpired(DateTime expired)
		{
			TimeSpan restTime = expired - DateTime.Now;
			var totalMinutes = restTime.TotalMinutes;
			if (totalMinutes < 0)
				return true;
			return false;
		}
	}
}
