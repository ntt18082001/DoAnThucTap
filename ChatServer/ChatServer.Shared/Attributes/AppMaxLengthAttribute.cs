using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppMaxLengthAttribute : MaxLengthAttribute
	{
		public AppMaxLengthAttribute() : base()
		{
		}

		public AppMaxLengthAttribute(int length) : base(length)
		{
			this.ErrorMessage = string.Format(AttributeErrMesg.MAXLEN, length);
		}
	}
}
