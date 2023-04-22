using ChatServer.Shared.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Shared.Attributes
{
	public class AppConfirmPwdAttribute : CompareAttribute
	{
		public AppConfirmPwdAttribute(string otherProperty = "Password") : base(otherProperty)
		{
			this.ErrorMessage = AttributeErrMesg.CONFIRM_PWD;
		}
	}
}
