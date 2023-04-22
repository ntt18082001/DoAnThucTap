using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Api.Common.Attributes
{
    public class AppAuthorizeAttribute : AuthorizeAttribute
    {
        public AppAuthorizeAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
