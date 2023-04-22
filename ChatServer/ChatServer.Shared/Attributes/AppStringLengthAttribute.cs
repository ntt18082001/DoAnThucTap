using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppStringLengthAttribute : StringLengthAttribute
	{
		public AppStringLengthAttribute(int minimumLength, int maximumLength) : base(maximumLength)
		{
			this.MinimumLength = minimumLength;
			this.ErrorMessage = string.Format(AttributeErrMesg.STRING_LEN, minimumLength, maximumLength);
		}
	}
}
