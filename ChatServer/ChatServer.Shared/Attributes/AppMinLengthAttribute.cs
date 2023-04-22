using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppMinLengthAttribute : MinLengthAttribute
	{
		public AppMinLengthAttribute(int length) : base(length)
		{
			this.ErrorMessage = string.Format(AttributeErrMesg.MINLEN, length);
		}
	}
}
