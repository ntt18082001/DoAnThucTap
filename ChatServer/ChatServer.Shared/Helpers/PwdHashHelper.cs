using ChatServer.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Helpers
{
	public static class PwdHashHelper
	{
		public static byte[] HashHMACSHA512WithKey(string pwd, byte[] key)
		{
			HMACSHA512 hmac = new(key);
			var pwdByte = Encoding.UTF8.GetBytes(pwd);
			return hmac.ComputeHash(pwdByte);
		}

		public static HashResult HashHMACSHA512(string pwd)
		{
			var hashResult = new HashResult();
			HMACSHA512 hmac = new();
			var pwdByte = Encoding.UTF8.GetBytes(pwd);
			hashResult.Value = hmac.ComputeHash(pwdByte);
			hashResult.Key = hmac.Key;
			return hashResult;
		}
	}
}
