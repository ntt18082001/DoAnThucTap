using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppRequiredAttribute : RequiredAttribute
	{
		public AppRequiredAttribute() : base()
		{
			this.ErrorMessage = AttributeErrMesg.REQUIRED;
		}
	}
}
