using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppRangeAttribute : RangeAttribute
	{
		public AppRangeAttribute(double minimum, double maximum) : base(minimum, maximum)
		{
			this.ErrorMessage = string.Format(AttributeErrMesg.RANGE, minimum, maximum);
		}
	}
}
