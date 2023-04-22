using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppRegexAttribute : RegularExpressionAttribute
	{
		public AppRegexAttribute(string pattern) : base(pattern)
		{
			this.ErrorMessage = AttributeErrMesg.REGEX;
		}
	}
}
