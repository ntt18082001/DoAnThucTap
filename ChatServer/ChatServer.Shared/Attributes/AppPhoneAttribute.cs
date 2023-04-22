using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppPhoneAttribute : RegularExpressionAttribute
	{
		public AppPhoneAttribute(string pattern = @"^\+*\d{10,15}$") : base(pattern)
		{
			this.ErrorMessage = AttributeErrMesg.PHONE;
		}
	}
}
